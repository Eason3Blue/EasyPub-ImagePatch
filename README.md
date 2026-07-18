# EasyPub Image Support Patch

![License](https://img.shields.io/badge/license-MIT-green)
![Platform](https://img.shields.io/badge/platform-Windows-blue)
![Target](https://img.shields.io/badge/EasyPub-1.5.0.0-orange)

**EasyPub Image Support Patch** 是针对 `EasyPub 1.5.0.0` 制作的非官方功能增强补丁。

本补丁为 EasyPub 增加了对 TXT 中图片标记的识别能力，使其能够在 TXT 转 EPUB 时自动下载图片并嵌入生成的电子书中。

> 本项目不是 EasyPub 官方项目。
>
> EasyPub 原软件版权归原作者 **lucida** 所有。
>
> 本项目仅提供针对特定版本的功能增强补丁。

---

## ✨ 新增功能

相比原版 EasyPub，本补丁增加：

### 图片标签支持

支持以下格式：

### HTML 图片

```html
<img src="https://example.com/image.jpg">
```

### Markdown 图片

```markdown
![图片描述](https://example.com/image.jpg)
```

### 图片 URL

支持直接识别：

```
https://example.com/image.jpg
```

---

## 🖼️ 图片处理

补丁会自动：

- 提取图片 URL
- 下载网络图片
- 保存到 EPUB 内部资源目录
- 自动生成 XHTML 图片标签
- 保持 EPUB 图片引用关系


支持格式：

- JPG / JPEG
- PNG
- GIF
- WEBP
- BMP
- SVG


---

## 📦 使用方法

### 1. 准备原版 EasyPub

请准备：

```
EasyPub.exe
```

版本要求：

```
EasyPub 1.5.0.0
```

其他版本可能无法使用本补丁。


---

### 2. 运行补丁程序

运行：

```
EasyPubPatch.exe
```

按照提示选择：

```
EasyPub.exe
```

补丁程序会自动生成增强版本。


---

### 3. 使用增强版 EasyPub

打开生成后的程序：

即可正常进行 TXT → EPUB 转换。


---

# 📌 注意事项

## 版本限制

当前仅支持：

```
EasyPub 1.5.0.0
```

如果你的 EasyPub 版本不同：

请勿强制使用补丁。


---

## 网络要求

转换包含网络图片的 TXT 时：

- 需要联网
- 图片服务器需要允许访问


---

## 图片版权

本工具仅提供技术支持。

TXT 文件中的图片版权属于原作者。

请确保你拥有合法使用这些图片的权利。


---

# 🔧 技术说明

本项目通过对 EasyPub 程序进行兼容性分析和功能扩展，实现图片资源解析功能。

主要修改内容：

- TXT 内容解析流程
- 图片标签识别
- 图片下载处理
- EPUB 图片资源生成


---

# 📖 致谢

感谢：

**lucida**

开发 EasyPub。

EasyPub 是一款优秀的 TXT 转 EPUB 工具，本补丁仅针对其图片处理能力进行了增强。

---

# 📜 License

本仓库中的：

- 补丁程序
- 自动化工具
- 脚本代码

采用：

MIT License


EasyPub 本体不属于本项目，其版权归原作者所有。
