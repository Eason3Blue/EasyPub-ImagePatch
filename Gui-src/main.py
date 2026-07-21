import os
import sys
import hashlib
import shutil
import subprocess
import webbrowser                     # 新增
import tkinter as tk
from tkinter import filedialog, messagebox

EXPECTED_SHA256 = "E5EC2B28E7E0CB5AB99A713B412E51B5EC761E6B9B29D09A239CDCCB5CE5042B"

class PatcherApp:
    def __init__(self, root):
        self.root = root
        self.root.title("EasyPub 补丁工具")
        self.root.resizable(False, False)

        # 程序所在目录
        self.app_dir = os.path.dirname(os.path.abspath(sys.argv[0]))

        # xdelta3 默认路径
        default_xdelta = os.path.join(self.app_dir, "xdelta3.exe")
        # 补丁默认路径
        default_patch = os.path.join(self.app_dir, "EasyPub_ImagePatch.xdelta")

        # ---- xdelta3 路径 ----
        tk.Label(root, text="xdelta3 路径:").grid(row=0, column=0, sticky="e", padx=5, pady=5)
        self.xdelta_var = tk.StringVar(value=default_xdelta)
        self.entry_xdelta = tk.Entry(root, textvariable=self.xdelta_var, width=50)
        self.entry_xdelta.grid(row=0, column=1, padx=5, pady=5, columnspan=2)

        # ---- 补丁路径 ----
        tk.Label(root, text="补丁文件路径:").grid(row=1, column=0, sticky="e", padx=5, pady=5)
        self.patch_var = tk.StringVar(value=default_patch)
        self.entry_patch = tk.Entry(root, textvariable=self.patch_var, width=50)
        self.entry_patch.grid(row=1, column=1, padx=5, pady=5, columnspan=2)

        # ---- 原 EasyPub.exe 路径 ----
        tk.Label(root, text="EasyPub.exe 路径:").grid(row=2, column=0, sticky="e", padx=5, pady=5)
        self.ezpub_var = tk.StringVar()
        self.entry_ezpub = tk.Entry(root, textvariable=self.ezpub_var, width=50)
        self.entry_ezpub.grid(row=2, column=1, padx=5, pady=5)
        self.btn_browse = tk.Button(root, text="浏览...", command=self.browse_ezpub)
        self.btn_browse.grid(row=2, column=2, padx=5, pady=5)

        # 校验状态显示
        self.lbl_verify = tk.Label(root, text="尚未选择文件", fg="gray")
        self.lbl_verify.grid(row=3, column=1, sticky="w", padx=5)

        # ---- 应用补丁按钮 ----
        self.btn_apply = tk.Button(root, text="应用补丁", command=self.apply_patch, state="disabled", bg="#4CAF50", fg="white")
        self.btn_apply.grid(row=4, column=1, pady=15, sticky="w")

        # ---- 项目主页链接（新增） ----
        link_label = tk.Label(root, text="项目主页", fg="blue", cursor="hand2", font=("", 9, "underline"))
        link_label.grid(row=5, column=0, columnspan=3, pady=(0, 10), sticky="e", padx=(0, 10))
        link_label.bind("<Button-1>", lambda e: webbrowser.open_new("https://github.com/Eason3Blue/EasyPub-ImagePatch"))

        # 文件是否通过校验的标志
        self.sha256_ok = False

    def browse_ezpub(self):
        """选择 EasyPub.exe 文件，并进行 SHA256 校验"""
        file_path = filedialog.askopenfilename(
            title="选择 EasyPub.exe (原版 1.5.0.0)",
            filetypes=[("可执行文件", "*.exe"), ("所有文件", "*.*")]
        )
        if not file_path:
            return

        self.ezpub_var.set(file_path)
        # 计算 SHA256
        try:
            with open(file_path, "rb") as f:
                content = f.read()
            sha256 = hashlib.sha256(content).hexdigest()
            if sha256.lower() == EXPECTED_SHA256.lower():
                self.lbl_verify.config(text="✔ SHA256 校验通过，文件正确", fg="green")
                self.sha256_ok = True
                self.btn_apply.config(state="normal")
            else:
                self.lbl_verify.config(text="✘ SHA256 校验失败！文件版本不符", fg="red")
                self.sha256_ok = False
                self.btn_apply.config(state="disabled")
                messagebox.showerror("校验失败", "所选文件的 SHA256 与预期值不符，请确认是否为 EasyPub 1.5.0.0 原版程序。")
        except Exception as e:
            messagebox.showerror("错误", f"读取文件失败: {e}")
            self.sha256_ok = False
            self.btn_apply.config(state="disabled")

    def apply_patch(self):
        """备份原文件，应用补丁，生成新 EasyPub.exe"""
        # 检查各路径是否存在
        xdelta_path = self.xdelta_var.get()
        patch_path = self.patch_var.get()
        ezpub_path = self.ezpub_var.get()

        if not os.path.isfile(xdelta_path):
            messagebox.showerror("错误", f"xdelta3.exe 不存在:\n{xdelta_path}")
            return
        if not os.path.isfile(patch_path):
            messagebox.showerror("错误", f"补丁文件不存在:\n{patch_path}")
            return
        if not os.path.isfile(ezpub_path):
            messagebox.showerror("错误", f"EasyPub.exe 不存在:\n{ezpub_path}")
            return
        if not self.sha256_ok:
            messagebox.showerror("错误", "SHA256 未通过校验，无法应用补丁。")
            return

        # 再次确认操作
        if not messagebox.askyesno("确认", "即将应用补丁，原 EasyPub.exe 将被修改。\n\n是否继续？"):
            return

        # 创建备份（.bak）
        backup_path = ezpub_path + ".bak"
        try:
            shutil.copy2(ezpub_path, backup_path)
        except Exception as e:
            messagebox.showerror("错误", f"无法创建备份文件:\n{e}")
            return

        # 应用补丁，先输出到临时文件
        temp_output = ezpub_path + ".patched.tmp"
        cmd = [xdelta_path, "-d", "-s", ezpub_path, patch_path, temp_output]
        try:
            result = subprocess.run(cmd, capture_output=True, text=True)
            if result.returncode != 0:
                messagebox.showerror("补丁失败",
                                     f"xdelta3 返回错误:\n{result.stderr.strip()}\n\n原文件已备份为 .bak，未受影响。")
                # 删除可能残存的临时文件
                if os.path.exists(temp_output):
                    os.remove(temp_output)
                return
        except Exception as e:
            messagebox.showerror("错误", f"执行 xdelta3 时出错:\n{e}")
            if os.path.exists(temp_output):
                os.remove(temp_output)
            return

        # 替换原文件
        try:
            # 删除原 exe，将临时文件重命名/移动
            if os.path.exists(ezpub_path):
                os.remove(ezpub_path)
            os.rename(temp_output, ezpub_path)
        except Exception as e:
            messagebox.showerror("错误", f"无法完成文件替换:\n{e}\n临时文件保存在: {temp_output}")
            return

        messagebox.showinfo("成功", "补丁应用成功！\n原 EasyPub.exe 已更新，备份文件为 EasyPub.exe.bak。")


def show_disclaimer():
    """程序启动时的提示框"""
    root = tk.Tk()
    root.withdraw()  # 隐藏主窗口
    messagebox.showinfo("提示",
                        "欢迎使用 EasyPub 图片支持增强补丁\n\n检测到本补丁将为 EasyPub 1.5.0.0 添加以下功能：\n\n• 支持 HTML <img> 图片标签\n• 支持 Markdown 图片语法\n• 自动下载网络图片\n• 自动将图片嵌入 EPUB 文件\n\n注意事项：\n1. 本补丁仅适用于 EasyPub 1.5.0.0。\n2. 应用补丁前建议备份原始 EasyPub.exe。\n3. 请确保您拥有合法使用 TXT 文件及其中图片资源的权限。\n\n声明：\n本补丁为非官方功能增强项目。\nEasyPub 原软件版权归原作者 lucida 所有。\n\n点击“确定”开始应用补丁。")
    root.destroy()


if __name__ == "__main__":
    show_disclaimer()

    # 创建主窗口
    root = tk.Tk()
    app = PatcherApp(root)
    root.mainloop()