using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml;
using EasyPub.Properties;
using Ionic.Zip;
using ns0;
using ns1;
using ns10;
using ns11;
using ns12;
using ns3;
using ns4;
using ns5;
using ns6;
using ns7;
using ns8;
using ns9;
using SmartAssembly.MemoryManagement;

namespace ns2
{
	// Token: 0x020000CA RID: 202
	internal sealed class Class34
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0002F684 File Offset: 0x0002D884
		static void smethod_0(Class10.Class11 class11_0)
		{
			string text = class11_0.stringBuilder_1.ToString().Trim();
			string text2 = text.Replace("&nbsp;", string.Empty);
			if (text2.Length != 0)
			{
				class11_0.int_0 = 0;
				class11_0.stringBuilder_0.AppendLine(text);
			}
			class11_0.stringBuilder_1.Length = 0;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002F6DC File Offset: 0x0002D8DC
		static byte[] smethod_1(byte[] byte_0, Class13 class13_0, int int_0, byte[] byte_1)
		{
			Buffer.BlockCopy(byte_1, 0, byte_0, int_0, byte_1.Length);
			return byte_0;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002F6F8 File Offset: 0x0002D8F8
		static bool smethod_2(string string_0, ref Struct3 struct3_0, ref Struct2 struct2_0)
		{
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			string text = directoryName + "\\" + string_0;
			if (!File.Exists(text))
			{
				Class34.smethod_153(Form.ActiveForm, string.Format("配置文件 {0} 不存在，程序退出", string_0), "EasyPub");
				Environment.Exit(0);
			}
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(text);
			}
			catch (Exception)
			{
				Class34.smethod_153(Form.ActiveForm, string.Format("加载配置文件{0}出错，程序退出", string_0), "EasyPub");
				Environment.Exit(0);
			}
			int num = Class34.smethod_60(xmlDocument, "/EasyPubConfig/XMLVersion", 1);
			if (num < 2)
			{
				Class34.smethod_153(Form.ActiveForm, string.Format("配置文件{0}版本过低，请重新下载{0}\n点击”确定“退出程序", string_0), "EasyPub");
				Environment.Exit(0);
			}
			string text2 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/eReadersConfig", "ereaders.xml");
			struct3_0.string_24 = text2;
			if (!File.Exists(directoryName + "\\" + text2))
			{
				Class34.smethod_153(Form.ActiveForm, string.Format("阅读器字体定义文件{0}不存在，程序退出", text2), "EasyPub");
				Environment.Exit(0);
			}
			XmlDocument xmlDocument2 = new XmlDocument();
			try
			{
				xmlDocument2.Load(directoryName + "\\" + text2);
			}
			catch (Exception ex)
			{
				Class34.smethod_169(string.Format("读取阅读器字体定义文件{0}出错\n{1}\n点击“确定”退出程序", text2, ex.Message), "EasyPub");
				Environment.Exit(0);
			}
			struct3_0.list_3 = new List<string>(10);
			struct3_0.list_1 = new List<string>(10);
			XmlNodeList xmlNodeList;
			try
			{
				xmlNodeList = xmlDocument2.SelectNodes("/EasyPubConfig/eReaders/model");
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string text3 = "";
					struct3_0.list_3.Add(xmlNode["name"].InnerText);
					XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("font");
					foreach (object obj2 in xmlNodeList2)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						text3 = text3 + xmlNode2.InnerText + "|";
					}
					struct3_0.list_1.Add(text3);
				}
			}
			catch (Exception ex2)
			{
				Class34.smethod_153(Form.ActiveForm, string.Format("读取阅读器字体定义文件{0}出错\n{1}\n点击“确定”退出程序", text2, ex2.Message), "EasyPub");
				Environment.Exit(0);
			}
			xmlDocument2 = null;
			struct3_0.list_0 = new List<string>(10);
			struct3_0.list_2 = new List<string>(10);
			xmlNodeList = xmlDocument.SelectNodes("/EasyPubConfig/MyRegExp/AdditionalReg");
			foreach (object obj3 in xmlNodeList)
			{
				XmlNode xmlNode3 = (XmlNode)obj3;
				XmlNodeList xmlNodeList3 = xmlNode3.SelectNodes("data");
				foreach (object obj4 in xmlNodeList3)
				{
					XmlNode xmlNode4 = (XmlNode)obj4;
					if (xmlNode4.InnerText.Trim() != "")
					{
						struct3_0.list_0.Add(xmlNode4.InnerText);
					}
				}
			}
			xmlNodeList = xmlDocument.SelectNodes("/EasyPubConfig/MyRegExp/FullReg");
			foreach (object obj5 in xmlNodeList)
			{
				XmlNode xmlNode5 = (XmlNode)obj5;
				XmlNodeList xmlNodeList4 = xmlNode5.SelectNodes("data");
				foreach (object obj6 in xmlNodeList4)
				{
					XmlNode xmlNode6 = (XmlNode)obj6;
					struct3_0.list_2.Add(xmlNode6.InnerText);
				}
			}
			struct3_0.string_23 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/splitmode", "0");
			struct3_0.int_13 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/splitcount", 1);
			struct3_0.string_6 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/top", "0");
			struct3_0.string_7 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/bottom", "0");
			struct3_0.string_8 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/left", "0");
			struct3_0.string_9 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/right", "0");
			struct3_0.int_2 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/pagetopunit", 0);
			struct3_0.int_3 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/pagebottomunit", 0);
			struct3_0.int_4 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/pageleftunit", 0);
			struct3_0.int_5 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/pagerightunit", 0);
			struct3_0.string_11 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/lineheight", "110");
			struct3_0.string_18 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/fontsize", "100");
			struct3_0.string_10 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/indent", "0");
			struct3_0.string_12 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/margintop", "0");
			struct3_0.int_6 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/margintopunit", 0);
			struct3_0.int_7 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/removeblankline", 0);
			struct3_0.string_13 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/addspace", "0");
			struct3_0.int_8 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/addspacecount", 2);
			struct3_0.int_10 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/coverstyle", 0);
			struct3_0.string_14 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/titlefont", "50");
			struct3_0.string_15 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/authorfont", "25");
			struct3_0.string_19 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/fonttype", "0");
			struct3_0.int_11 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/machineid", 0);
			struct3_0.string_16 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/font_customized", "");
			struct3_0.string_17 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/font_embedded", "");
			struct3_0.int_12 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/font_subsetting", 0);
			struct3_0.string_0 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/editor", "notepad.exe");
			struct3_0.string_22 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/outputfolder", "");
			struct3_0.string_20 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/cssoverwrite", "0");
			struct3_0.string_1 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/forcetextcover", "0");
			struct3_0.int_14 = Math.Abs(Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/posx1", 200));
			struct3_0.int_15 = Math.Abs(Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/posy1", 200));
			struct3_0.int_16 = Math.Abs(Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/posx2", 200));
			struct3_0.int_17 = Math.Abs(Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/posy2", 200));
			int width = SystemInformation.VirtualScreen.Width;
			int height = SystemInformation.VirtualScreen.Height;
			if (struct3_0.int_14 > width)
			{
				struct3_0.int_14 = 200;
			}
			if (struct3_0.int_16 > width)
			{
				struct3_0.int_16 = 200;
			}
			if (struct3_0.int_15 > height)
			{
				struct3_0.int_15 = 200;
			}
			if (struct3_0.int_17 > height)
			{
				struct3_0.int_17 = 200;
			}
			struct3_0.int_18 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/automark", -5);
			struct3_0.string_21 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/RecentOptions/savecss", "1");
			struct3_0.int_0 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/simple_reg_leadingspace", 0);
			string text4 = "/EasyPubConfig/RecentOptions/simple_reg_p1";
			string text5 = "第";
			struct3_0.string_2 = Class34.smethod_108(text4, xmlDocument, text5);
			struct3_0.int_1 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/simple_reg_p2", 0);
			text4 = "/EasyPubConfig/RecentOptions/simple_reg_p3";
			text5 = "章";
			struct3_0.string_3 = Class34.smethod_108(text4, xmlDocument, text5);
			text4 = "/EasyPubConfig/RecentOptions/simple_reg_ext";
			text5 = "";
			struct3_0.string_4 = Class34.smethod_108(text4, xmlDocument, text5);
			text4 = "/EasyPubConfig/RecentOptions/full_reg";
			text5 = "";
			struct3_0.string_5 = Class34.smethod_108(text4, xmlDocument, text5);
			struct3_0.int_9 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/RecentOptions/textalign", 0);
			struct2_0.int_0 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/silentmode", 0);
			struct2_0.int_6 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/tocspace", 0);
			struct2_0.int_7 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/outputtosrc", 0);
			struct2_0.int_1 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/enable_htmlrawtag", 0);
			struct2_0.string_0 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/AdvancedOptions/htmlrawtag", "##");
			struct2_0.int_2 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/enable_tempdir", 0);
			struct2_0.string_1 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/AdvancedOptions/tempdir", "");
			struct2_0.int_3 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/flowsize", 200);
			if (struct2_0.int_3 < 20)
			{
				struct2_0.int_3 = 20;
			}
			Form1.int_0 = struct2_0.int_3 * 1000;
			struct2_0.int_4 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/screenwidth", 540);
			struct2_0.int_5 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/screenheight", 720);
			struct2_0.int_8 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/emptychapterstyle", 1);
			struct2_0.int_9 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/outputformat", 0);
			struct2_0.int_10 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/mobistrip", 1);
			struct2_0.int_11 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/mobiperiodical", 0);
			struct2_0.int_12 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/mobiforceen", 0);
			struct2_0.int_13 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/mobisync", 1);
			struct2_0.int_14 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/asinstyle", 0);
			struct2_0.string_2 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/AdvancedOptions/mobiasin", "");
			struct2_0.string_3 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/AdvancedOptions/kindlegenexe", "kindlegen.exe");
			struct2_0.int_15 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/kindlegencompress", 1);
			struct2_0.int_16 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/mobiformat", 0);
			struct2_0.string_4 = Class34.smethod_58(xmlDocument, "/EasyPubConfig/AdvancedOptions/kindlegenoption", "");
			struct2_0.int_17 = Class34.smethod_60(xmlDocument, "/EasyPubConfig/AdvancedOptions/alwaysontop", 0);
			xmlDocument = null;
			return true;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00030154 File Offset: 0x0002E354
		static void smethod_3(ToolTip toolTip_0, int int_0)
		{
			IntPtr intPtr = Class34.smethod_61(toolTip_0);
			Class34.SendMessage_2(new HandleRef(toolTip_0, intPtr), 1048, 0, int_0);
			int num = ColorTranslator.ToWin32(SystemColors.Info);
			Class34.SendMessage_2(new HandleRef(toolTip_0, intPtr), 1043, num, 0);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0003019C File Offset: 0x0002E39C
		static int smethod_4(XmlNode xmlNode_0, Class3 class3_0, XmlNode xmlNode_1)
		{
			int num = 1;
			XmlNode xmlNode = xmlNode_0;
			while (xmlNode != xmlNode_1 && xmlNode.ParentNode != null)
			{
				xmlNode = xmlNode.ParentNode;
				num++;
			}
			return num;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000301CC File Offset: 0x0002E3CC
		static void smethod_5(Form1 form1_0, string string_0)
		{
			string text = "#C1CCC0";
			string text2 = "#586357";
			string text3 = "#6B766A";
			string text4 = "#939E92";
			string text5 = "#4E594D";
			StreamWriter streamWriter = new StreamWriter(form1_0.string_1 + "\\OEBPS\\" + string_0);
			if (form1_0.radioButton_7.Checked)
			{
				streamWriter.Write(form1_0.textBox_4.Text);
				streamWriter.WriteLine();
				streamWriter.Flush();
				streamWriter.Close();
				return;
			}
			streamWriter.WriteLine("/*  Generated by EasyPub  */");
			streamWriter.WriteLine("/*  此css由EasyPub自动生成  */");
			streamWriter.WriteLine("/*  部分参考老牛中文样式  */");
			streamWriter.WriteLine();
			if (form1_0.radioButton_8.Checked)
			{
				streamWriter.Write(form1_0.textBox_4.Text);
				streamWriter.WriteLine();
			}
			if (form1_0.radioButton_3.Checked)
			{
				streamWriter.WriteLine("@font-face {");
				streamWriter.WriteLine("      font-family: \"easypub\";");
				string text6 = form1_0.struct3_0.list_1[form1_0.comboBox_1.SelectedIndex];
				string[] array = text6.Split(new char[] { '|' });
				if (array.Length == 2)
				{
					streamWriter.WriteLine("      src: url(" + array[0] + ");");
				}
				else if (array.Length == 3)
				{
					streamWriter.WriteLine("      src: url(" + array[0] + "),");
					streamWriter.WriteLine("           url(" + array[1] + ");");
				}
				else
				{
					streamWriter.WriteLine("      src: url(" + array[0] + "),");
					for (int i = 1; i < array.Length - 2; i++)
					{
						streamWriter.WriteLine("           url(" + array[i] + "),");
					}
					streamWriter.WriteLine("           url(" + array[array.Length - 2] + ");");
				}
				streamWriter.WriteLine("}");
			}
			else if (form1_0.radioButton_2.Checked)
			{
				streamWriter.WriteLine("@font-face {");
				streamWriter.WriteLine("      font-family: \"easypub\";");
				string text6 = form1_0.textBox_5.Text;
				streamWriter.WriteLine("      src: url(" + text6 + ");");
				streamWriter.WriteLine("}");
			}
			else if (form1_0.radioButton_1.Checked)
			{
				Class34.smethod_118(form1_0, "正在嵌入字体...", "normal");
				streamWriter.WriteLine("@font-face {");
				streamWriter.WriteLine("      font-family: \"easypub\";");
				string text6 = form1_0.struct1_0.string_2 + ".ttf";
				if (File.Exists(form1_0.textBox_3.Text))
				{
					if (form1_0.checkBox_0.Checked)
					{
						string text7 = form1_0.textBox_3.Text;
						string text8 = form1_0.string_1 + "\\OEBPS\\" + text6;
						string text9 = form1_0.textBox_0.Text;
						string text10 = "目录TableOfContents \u3000" + form1_0.struct1_0.string_0 + form1_0.struct1_0.string_5;
						Class34.smethod_143(text10, text8, text9, text7);
					}
					else
					{
						File.Copy(form1_0.textBox_3.Text, form1_0.string_1 + "\\OEBPS\\" + text6, true);
					}
				}
				streamWriter.WriteLine("      src: url(" + text6 + ");");
				streamWriter.WriteLine("}");
			}
			else
			{
				streamWriter.WriteLine();
			}
			streamWriter.WriteLine();
			if (!form1_0.bool_1 && (form1_0.comboBox_5.Text.Trim().ToUpper() != "X" || form1_0.comboBox_8.Text.Trim().ToUpper() != "X"))
			{
				streamWriter.WriteLine("@page { ");
				if (form1_0.comboBox_5.Text.Trim().ToUpper() != "X")
				{
					streamWriter.WriteLine("      margin-top: " + form1_0.comboBox_5.Text + form1_0.comboBox_23.Text + ";");
				}
				if (form1_0.comboBox_8.Text.Trim().ToUpper() != "X")
				{
					streamWriter.WriteLine("      margin-bottom: " + form1_0.comboBox_8.Text + form1_0.comboBox_26.Text + ";");
				}
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
			}
			streamWriter.WriteLine("body { ");
			if (!form1_0.radioButton_0.Checked)
			{
				streamWriter.WriteLine("      font-family: \"easypub\";");
			}
			streamWriter.WriteLine("      padding: 0;");
			if (form1_0.comboBox_7.Text.Trim().ToUpper() != "X")
			{
				streamWriter.WriteLine("      margin-left: " + form1_0.comboBox_7.Text + form1_0.comboBox_25.Text + ";");
			}
			if (form1_0.comboBox_6.Text.Trim().ToUpper() != "X")
			{
				streamWriter.WriteLine("      margin-right: " + form1_0.comboBox_6.Text + form1_0.comboBox_24.Text + ";");
			}
			streamWriter.WriteLine("      orphans: 0;");
			streamWriter.WriteLine("      widows: 0;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			streamWriter.WriteLine("p { ");
			if (!form1_0.radioButton_0.Checked)
			{
				streamWriter.WriteLine("      font-family: \"easypub\";");
			}
			if (form1_0.comboBox_10.Text.Trim().ToUpper() != "X")
			{
				streamWriter.WriteLine("      font-size: " + form1_0.comboBox_10.Text + "%;");
			}
			if (form1_0.comboBox_0.Text.Trim().ToUpper() != "X")
			{
				streamWriter.WriteLine("      line-height: " + form1_0.comboBox_0.Text + "%;");
			}
			if (form1_0.comboBox_9.Text.Trim().ToUpper() != "X")
			{
				streamWriter.WriteLine("      margin-top: " + form1_0.comboBox_9.Text + form1_0.comboBox_20.Text + ";");
				streamWriter.WriteLine("      margin-bottom: 0;");
			}
			streamWriter.WriteLine("      margin-left: 0;");
			streamWriter.WriteLine("      margin-right: 0;");
			streamWriter.WriteLine("      orphans: 0;");
			streamWriter.WriteLine("      widows: 0;");
			switch (form1_0.comboBox_14.SelectedIndex)
			{
			case 1:
				streamWriter.WriteLine("      text-align:justify;");
				break;
			case 2:
				streamWriter.WriteLine("      text-align:left;");
				break;
			case 3:
				streamWriter.WriteLine("      text-align:right;");
				break;
			case 4:
				streamWriter.WriteLine("      text-align:center;");
				break;
			}
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			if (form1_0.comboBox_11.Text.Trim().ToUpper() != "X")
			{
				streamWriter.WriteLine(".a { ");
				streamWriter.WriteLine("      text-indent: " + form1_0.comboBox_11.Text + "em;");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
			}
			streamWriter.WriteLine("div.centeredimage {");
			streamWriter.WriteLine("      text-align:center;");
			streamWriter.WriteLine("      display:block;");
			streamWriter.WriteLine("      margin-top: 0.5em;");
			streamWriter.WriteLine("      margin-bottom: 0.5em;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			streamWriter.WriteLine("img.attpic {");
			streamWriter.WriteLine("      border: 1px solid #000000;");
			streamWriter.WriteLine("      max-width: 100%;");
			streamWriter.WriteLine("      margin: 0;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			streamWriter.WriteLine(".booktitle {");
			streamWriter.WriteLine("      margin-top: 30%;");
			streamWriter.WriteLine("      margin-bottom: 0;");
			streamWriter.WriteLine("      border-style: none solid none none;");
			streamWriter.WriteLine("      border-width: 50px;");
			streamWriter.WriteLine("      border-color: " + text5 + ";");
			streamWriter.WriteLine("      font-size: 3em;");
			streamWriter.WriteLine("      line-height: 120%;");
			streamWriter.WriteLine("      text-align: right;");
			if (form1_0.radioButton_0.Checked)
			{
			}
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			streamWriter.WriteLine(".bookauthor {");
			streamWriter.WriteLine("      margin-top: 0;");
			streamWriter.WriteLine("      border-style: none solid none none;");
			streamWriter.WriteLine("      border-width: 50px;");
			streamWriter.WriteLine("      border-color: " + text5 + ";");
			streamWriter.WriteLine("      page-break-after: always;");
			streamWriter.WriteLine("      font-size: large;");
			streamWriter.WriteLine("      line-height: 120%;");
			streamWriter.WriteLine("      text-align: right;");
			if (form1_0.radioButton_0.Checked)
			{
			}
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			if (!form1_0.bool_1)
			{
				streamWriter.WriteLine(".titletoc, .titlel1top, .titlel1std,.titlel2top, .titlel2std,.titlel3top, .titlel3std,.titlel4std {");
				streamWriter.WriteLine("      margin-top: 0;");
				streamWriter.WriteLine("      border-style: none double none solid;");
				streamWriter.WriteLine("      border-width: 0px 5px 0px 20px;");
				streamWriter.WriteLine("      border-color: " + text2 + ";");
				streamWriter.WriteLine("      background-color: " + text + ";");
				streamWriter.WriteLine("      padding: 45px 5px 5px 5px;");
				streamWriter.WriteLine("      font-size: x-large;");
				streamWriter.WriteLine("      line-height: 115%;");
				streamWriter.WriteLine("      text-align: justify;");
				bool @checked = form1_0.radioButton_0.Checked;
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
				streamWriter.WriteLine(".titlel1single,.titlel2single,.titlel3single {");
				streamWriter.WriteLine("      margin-top: 35%;");
				streamWriter.WriteLine("      border-style: none solid none none;");
				streamWriter.WriteLine("      border-width: 30px;");
				streamWriter.WriteLine("      border-color: " + text5 + ";");
				streamWriter.WriteLine("      padding: 30px 5px 5px 5px;");
				streamWriter.WriteLine("      font-size: x-large;");
				streamWriter.WriteLine("      line-height: 125%;");
				streamWriter.WriteLine("      text-align: right;");
				bool checked2 = form1_0.radioButton_0.Checked;
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
			}
			else
			{
				streamWriter.WriteLine("@media not amzn-mobi {");
				streamWriter.WriteLine(".titletoc, .titlel1top, .titlel1std,.titlel2top, .titlel2std,.titlel3top, .titlel3std,.titlel4std {");
				streamWriter.WriteLine("      margin-top: 0;");
				streamWriter.WriteLine("      margin-bottom: 1.2em;");
				streamWriter.WriteLine("      border-style: none none dotted none;");
				streamWriter.WriteLine("      border-width: 0px 0px 1px 0px;");
				streamWriter.WriteLine("      border-color: " + text + ";");
				streamWriter.WriteLine("      padding: 10px 0 10px 0;");
				streamWriter.WriteLine("      font-size: x-large;");
				streamWriter.WriteLine("      line-height: 125%;");
				streamWriter.WriteLine("      text-align: center;");
				streamWriter.WriteLine("      text-shadow: 1px 1px 1px #333;");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
				streamWriter.WriteLine(".titlel1single,.titlel2single,.titlel3single {");
				streamWriter.WriteLine("      margin-top: 35%;");
				streamWriter.WriteLine("      border-style: none solid none none;");
				streamWriter.WriteLine("      border-width: 30px;");
				streamWriter.WriteLine("      border-color: " + text5 + ";");
				streamWriter.WriteLine("      padding: 30px 5px 5px 5px;");
				streamWriter.WriteLine("      font-size: x-large;");
				streamWriter.WriteLine("      line-height: 110%;");
				streamWriter.WriteLine("      text-align: right;");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
				streamWriter.WriteLine("@media amzn-mobi {");
				streamWriter.WriteLine(".titletoc, .titlel1top, .titlel1std,.titlel2top, .titlel2std,.titlel3top, .titlel3std,.titlel4std {");
				streamWriter.WriteLine("      margin-top: 0;");
				streamWriter.WriteLine("      padding-top: 20px;");
				streamWriter.WriteLine("      font-size: x-large;");
				streamWriter.WriteLine("      line-height: 125%;");
				streamWriter.WriteLine("      text-align: center;");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
				streamWriter.WriteLine(".titlel1single,.titlel2single,.titlel3single {");
				streamWriter.WriteLine("      margin-top: 35%;");
				streamWriter.WriteLine("      line-height: 110%;");
				streamWriter.WriteLine("      font-size: x-large;");
				streamWriter.WriteLine("      text-align: center;");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
			}
			streamWriter.WriteLine(".toc {");
			if (form1_0.bool_1)
			{
				streamWriter.WriteLine("      margin-left:12%;");
			}
			else
			{
				streamWriter.WriteLine("      margin-left:16%;");
			}
			streamWriter.WriteLine("      padding:0px;");
			streamWriter.WriteLine("      line-height:130%;");
			streamWriter.WriteLine("      text-align: justify;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			streamWriter.WriteLine(".toc a { text-decoration: none; color: #000000; }");
			streamWriter.WriteLine();
			streamWriter.WriteLine(".tocl1 {");
			streamWriter.WriteLine("      margin-top:0.5em;");
			streamWriter.WriteLine("      margin-left:-30px;");
			streamWriter.WriteLine("      border-style: none double double solid;");
			streamWriter.WriteLine("      border-width: 0px 5px 2px 20px;");
			streamWriter.WriteLine("      border-color: " + text3 + ";");
			streamWriter.WriteLine("      line-height: 135%;");
			streamWriter.WriteLine("      font-size: 132%;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			if (!form1_0.bool_1)
			{
				streamWriter.WriteLine(".tocl2 {");
				streamWriter.WriteLine("      margin-top: 0.5em;");
				streamWriter.WriteLine("      margin-left:-20px;");
				streamWriter.WriteLine("      border-style: none double none solid;");
				streamWriter.WriteLine("      border-width: 0px 2px 0px 10px;");
				streamWriter.WriteLine("      border-color: " + text4 + ";");
				streamWriter.WriteLine("      line-height: 123%;");
				streamWriter.WriteLine("      font-size: 120%;");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
			}
			else
			{
				streamWriter.WriteLine(".tocl2 {");
				streamWriter.WriteLine("      margin-top: 0.5em;");
				streamWriter.WriteLine("      margin-left:-20px;");
				streamWriter.WriteLine("      border-style: none none none none;");
				streamWriter.WriteLine("      line-height: 123%;");
				streamWriter.WriteLine("      font-size: 120%;");
				streamWriter.WriteLine("}");
				streamWriter.WriteLine();
				streamWriter.WriteLine();
			}
			streamWriter.WriteLine(".tocl3 {");
			streamWriter.WriteLine("      margin-top: 0.5em;");
			streamWriter.WriteLine("      margin-left:-20px;");
			streamWriter.WriteLine("      border-style: none double none solid;");
			streamWriter.WriteLine("      border-width: 0px 2px 0px 8px;");
			streamWriter.WriteLine("      border-color: " + text4 + ";");
			streamWriter.WriteLine("      line-height: 112%;");
			streamWriter.WriteLine("      font-size: 109%;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			streamWriter.WriteLine(".tocl4 {");
			streamWriter.WriteLine("      margin-top: 0.5em;");
			streamWriter.WriteLine("      margin-left:-20px;");
			streamWriter.WriteLine("      border-style: none double none solid;");
			streamWriter.WriteLine("      border-width: 0px 2px 0px 6px;");
			streamWriter.WriteLine("      border-color: " + text4 + ";");
			streamWriter.WriteLine("      line-height: 115%;");
			streamWriter.WriteLine("      font-size: 110%;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			streamWriter.WriteLine(".subtoc {");
			streamWriter.WriteLine("      margin-left:15%;");
			streamWriter.WriteLine("      padding:0px;");
			streamWriter.WriteLine("      text-align: justify;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			streamWriter.WriteLine(".subtoclist {");
			streamWriter.WriteLine("      margin-top: 0.5em;");
			streamWriter.WriteLine("      margin-left:-20px;");
			streamWriter.WriteLine("      border-style: none double none solid;");
			streamWriter.WriteLine("      border-width: 0px 2px 0px 10px;");
			streamWriter.WriteLine("      border-color: " + text4 + ";");
			streamWriter.WriteLine("      line-height: 123%;");
			streamWriter.WriteLine("      font-size: 120%;");
			streamWriter.WriteLine("}");
			streamWriter.WriteLine();
			if (form1_0.radioButton_8.Checked)
			{
				streamWriter.Write(form1_0.textBox_4.Text);
				streamWriter.WriteLine();
			}
			streamWriter.Flush();
			streamWriter.Close();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00031248 File Offset: 0x0002F448
		static Image smethod_6(Size size_0, Form1 form1_0, Image image_0)
		{
			int width = image_0.Width;
			int height = image_0.Height;
			float num = (float)size_0.Width / (float)width;
			float num2 = (float)size_0.Height / (float)height;
			float num3;
			if (num2 < num)
			{
				num3 = num2;
			}
			else
			{
				num3 = num;
			}
			int num4 = (int)((float)width * num3);
			int num5 = (int)((float)height * num3);
			Bitmap bitmap = new Bitmap(num4, num5);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.DrawImage(image_0, 0, 0, num4, num5);
			graphics.Dispose();
			return bitmap;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000312DC File Offset: 0x0002F4DC
		static void smethod_7(Class10.Class11 class11_0, char char_0)
		{
			if (char_0 == '\u3000')
			{
				class11_0.stringBuilder_0.Append(char_0);
				return;
			}
			if (class11_0.bool_0)
			{
				class11_0.stringBuilder_0.Append(char_0);
				return;
			}
			if (char_0 == '\r')
			{
				return;
			}
			if (char_0 == '\n')
			{
				Class34.smethod_0(class11_0);
				return;
			}
			if (Class34.smethod_145(char_0))
			{
				int length = class11_0.stringBuilder_1.Length;
				if (length == 0 || !Class34.smethod_145(class11_0.stringBuilder_1[length - 1]))
				{
					class11_0.stringBuilder_1.Append(' ');
					return;
				}
			}
			else
			{
				class11_0.stringBuilder_1.Append(char_0);
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00008580 File Offset: 0x00006780
		static void smethod_8(Class27.Class29 class29_0, int int_0)
		{
			class29_0.uint_0 >>= int_0;
			class29_0.int_2 -= int_0;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00031370 File Offset: 0x0002F570
		static void smethod_9(Class27.Class30 class30_0, int int_0, int int_1)
		{
			if ((class30_0.int_1 += int_0) > 32768)
			{
				throw new InvalidOperationException();
			}
			int num = (class30_0.int_0 - int_1) & 32767;
			int num2 = 32768 - int_0;
			if (num > num2 || class30_0.int_0 >= num2)
			{
				Class34.smethod_162(class30_0, num, int_0, int_1);
				return;
			}
			if (int_0 <= int_1)
			{
				Array.Copy(class30_0.byte_0, num, class30_0.byte_0, class30_0.int_0, int_0);
				class30_0.int_0 += int_0;
				return;
			}
			while (int_0-- > 0)
			{
				class30_0.byte_0[class30_0.int_0++] = class30_0.byte_0[num++];
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000085A1 File Offset: 0x000067A1
		static bool smethod_10(string string_0, string string_1)
		{
			return Class34.smethod_51(string_1, string_0);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00031424 File Offset: 0x0002F624
		static void smethod_11(Form1 form1_0)
		{
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			if (form1_0.textBox_8.Enabled && form1_0.textBox_8.Text.Trim() != "")
			{
				form1_0.string_1 = form1_0.textBox_8.Text;
			}
			else
			{
				form1_0.string_1 = directoryName + "\\temp";
			}
			Form1.int_0 = int.Parse(form1_0.textBox_7.Text);
			if (Form1.int_0 < 20)
			{
				Form1.int_0 = 20;
			}
			Form1.int_0 *= 1000;
			if (Path.GetExtension(form1_0.comboBox_27.Text.ToLower()) == ".mobi")
			{
				Form1.int_0 = 204800000;
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000314EC File Offset: 0x0002F6EC
		static int smethod_12(Class27.Class31 class31_0, Class27.Class29 class29_0)
		{
			int num;
			if ((num = Class34.smethod_21(class29_0, 9)) >= 0)
			{
				int num2;
				if ((num2 = (int)class31_0.short_0[num]) >= 0)
				{
					Class34.smethod_8(class29_0, num2 & 15);
					return num2 >> 4;
				}
				int num3 = -(num2 >> 4);
				int num4 = num2 & 15;
				if ((num = Class34.smethod_21(class29_0, num4)) >= 0)
				{
					num2 = (int)class31_0.short_0[num3 | (num >> 9)];
					Class34.smethod_8(class29_0, num2 & 15);
					return num2 >> 4;
				}
				int int_ = class29_0.int_2;
				num = Class34.smethod_21(class29_0, int_);
				num2 = (int)class31_0.short_0[num3 | (num >> 9)];
				if ((num2 & 15) <= int_)
				{
					Class34.smethod_8(class29_0, num2 & 15);
					return num2 >> 4;
				}
				return -1;
			}
			else
			{
				int int_2 = class29_0.int_2;
				num = Class34.smethod_21(class29_0, int_2);
				int num2 = (int)class31_0.short_0[num];
				if (num2 >= 0 && (num2 & 15) <= int_2)
				{
					Class34.smethod_8(class29_0, num2 & 15);
					return num2 >> 4;
				}
				return -1;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00008A80 File Offset: 0x00006C80
		static byte[] smethod_13(ushort ushort_0)
		{
			byte[] bytes = BitConverter.GetBytes(ushort_0);
			Array.Reverse(bytes, 0, bytes.Length);
			return bytes;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000085AA File Offset: 0x000067AA
		static bool smethod_14(Class21 class21_0)
		{
			return Class34.smethod_82("", Enum0.const_10, class21_0, 0);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000315C4 File Offset: 0x0002F7C4
		static void smethod_15(Form1 form1_0, string string_0)
		{
			Class34.smethod_118(form1_0, "加入文件：" + string_0, "normal");
			if (!File.Exists(string_0))
			{
				Class34.smethod_118(form1_0, string_0 + "不存在!", "warning");
				return;
			}
			foreach (object obj in form1_0.listView_0.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.Text == string_0)
				{
					Class34.smethod_118(form1_0, "跳过文件：" + string_0, "normal");
					return;
				}
			}
			form1_0.listView_0.Items.Add(string_0);
			Class34.smethod_118(form1_0, "成功加入文件：" + string_0, "normal");
		}

		// Token: 0x060005FC RID: 1532
		[DllImport("user32.dll")]
		static extern bool GetWindowRect(IntPtr intptr_0, out Class4.Struct4 struct4_0);

		// Token: 0x060005FD RID: 1533 RVA: 0x000316A0 File Offset: 0x0002F8A0
		static Bitmap smethod_16()
		{
			object @object = Class34.smethod_112().GetObject("cover-orig", Class23.cultureInfo_0);
			return (Bitmap)@object;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000316C8 File Offset: 0x0002F8C8
		static byte[] smethod_17(byte[] byte_0, int int_0)
		{
			Tuple<int, int> tuple = Class34.smethod_81(byte_0, int_0);
			int item = tuple.Item1;
			int item2 = tuple.Item2;
			return byte_0.smethod_6(item, item2);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000085BD File Offset: 0x000067BD
		static ListViewItem smethod_18(EventArgs0 eventArgs0_0)
		{
			return eventArgs0_0.listViewItem_0;
		}

		// Token: 0x06000600 RID: 1536
		[DllImport("user32.dll", EntryPoint = "GetWindowRect")]
		static extern bool GetWindowRect_1(IntPtr intptr_0, ref Rectangle rectangle_0);

		// Token: 0x06000601 RID: 1537 RVA: 0x000316F4 File Offset: 0x0002F8F4
		static int smethod_19(Class27.Class30 class30_0, Class27.Class29 class29_0, int int_0)
		{
			int_0 = Math.Min(Math.Min(int_0, 32768 - class30_0.int_1), Class34.smethod_40(class29_0));
			int num = 32768 - class30_0.int_0;
			int num2;
			if (int_0 > num)
			{
				num2 = Class34.smethod_93(class29_0, class30_0.byte_0, class30_0.int_0, num);
				if (num2 == num)
				{
					num2 += Class34.smethod_93(class29_0, class30_0.byte_0, 0, int_0 - num);
				}
			}
			else
			{
				num2 = Class34.smethod_93(class29_0, class30_0.byte_0, class30_0.int_0, int_0);
			}
			class30_0.int_0 = (class30_0.int_0 + num2) & 32767;
			class30_0.int_1 += num2;
			return num2;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00031798 File Offset: 0x0002F998
		static string smethod_20(Form2 form2_0, string string_0)
		{
			string text = "^＋";
			return Regex.Replace(string_0, text, "");
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000317B8 File Offset: 0x0002F9B8
		static int smethod_21(Class27.Class29 class29_0, int int_0)
		{
			if (class29_0.int_2 < int_0)
			{
				if (class29_0.int_0 == class29_0.int_1)
				{
					return -1;
				}
				class29_0.uint_0 |= (uint)((uint)((int)(class29_0.byte_0[class29_0.int_0++] & byte.MaxValue) | ((int)(class29_0.byte_0[class29_0.int_0++] & byte.MaxValue) << 8)) << class29_0.int_2);
				class29_0.int_2 += 16;
			}
			return (int)((ulong)class29_0.uint_0 & (ulong)((long)((1 << int_0) - 1)));
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00031858 File Offset: 0x0002FA58
		static void smethod_22(string string_0, string string_1, string string_2, string string_3, int int_0, int int_1)
		{
			int num = Math.Abs(int_0);
			if (num == 0)
			{
				num = 50;
			}
			int num2 = Math.Abs(int_1);
			if (num2 == 0)
			{
				num2 = 25;
			}
			string text;
			if (string_0.Trim() != "")
			{
				text = string_0.Trim();
			}
			else
			{
				text = "Unknown Title";
			}
			string text2 = string_1.Trim();
			Font font;
			Font font2;
			if (File.Exists(string_2))
			{
				PrivateFontCollection privateFontCollection = new PrivateFontCollection();
				privateFontCollection.AddFontFile(string_2);
				font = new Font(privateFontCollection.Families[0], (float)num, FontStyle.Bold);
				font2 = new Font(privateFontCollection.Families[0], (float)num2, FontStyle.Bold);
				privateFontCollection.Dispose();
			}
			else
			{
				font = new Font("SimSun", (float)num, FontStyle.Bold);
				font2 = new Font("SimSun", (float)num2, FontStyle.Bold);
			}
			Bitmap bitmap = new Bitmap(600, 800);
			Graphics graphics = Graphics.FromImage(bitmap);
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Far;
			stringFormat.LineAlignment = StringAlignment.Center;
			stringFormat.Trimming = StringTrimming.None;
			graphics.Clear(global::System.Drawing.Color.White);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			RectangleF rectangleF = default(RectangleF);
			rectangleF.Location = new Point(30, 220);
			rectangleF.Size = new Size(520, (int)graphics.MeasureString(text, font, 520).Height);
			graphics.DrawString(text, font, global::System.Drawing.Brushes.Black, rectangleF, stringFormat);
			if (text2 != "")
			{
				RectangleF rectangleF2 = default(RectangleF);
				rectangleF2.Location = new Point(30, (int)rectangleF.Bottom + 20);
				rectangleF2.Size = new Size(520, (int)graphics.MeasureString(text2, font2, 520).Height);
				graphics.DrawString(text2, font2, global::System.Drawing.Brushes.Black, rectangleF2, stringFormat);
				graphics.FillRectangle(new SolidBrush(global::System.Drawing.Color.FromArgb(94, 94, 94)), 565f, 220f, 35f, rectangleF2.Bottom - rectangleF.Top);
			}
			else
			{
				graphics.FillRectangle(new SolidBrush(global::System.Drawing.Color.FromArgb(94, 94, 94)), 565f, 220f, 35f, rectangleF.Bottom - 220f);
			}
			font.Dispose();
			font2.Dispose();
			graphics.Flush();
			bitmap.Save(string_3);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00031AC4 File Offset: 0x0002FCC4
		static byte[] smethod_23(int int_0, byte[] byte_0, int int_1)
		{
			byte[] array = new byte[int_0 - int_1];
			Buffer.BlockCopy(byte_0, int_1, array, 0, int_0 - int_1);
			return array;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00008A14 File Offset: 0x00006C14
		static ushort smethod_24(byte[] byte_0, int int_0)
		{
			ushort num = BitConverter.ToUInt16(byte_0, int_0);
			return (ushort)(((int)(num & 255) << 8) | ((num & 65280) >> 8));
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00031AE8 File Offset: 0x0002FCE8
		static bool smethod_25(ref Struct3 struct3_0, string string_0, ref Struct2 struct2_0)
		{
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			string text = directoryName + "\\" + string_0;
			if (!File.Exists(text))
			{
				return false;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(text);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/splitmode", struct3_0.string_23);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/splitcount", struct3_0.int_13);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/top", struct3_0.string_6);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/pagetopunit", struct3_0.int_2);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/bottom", struct3_0.string_7);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/pagebottomunit", struct3_0.int_3);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/left", struct3_0.string_8);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/pageleftunit", struct3_0.int_4);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/right", struct3_0.string_9);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/pagerightunit", struct3_0.int_5);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/lineheight", struct3_0.string_11);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/fontsize", struct3_0.string_18);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/indent", struct3_0.string_10);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/margintop", struct3_0.string_12);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/margintopunit", struct3_0.int_6);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/removeblankline", struct3_0.int_7);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/textalign", struct3_0.int_9);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/coverstyle", struct3_0.int_10);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/titlefont", struct3_0.string_14);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/authorfont", struct3_0.string_15);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/fonttype", struct3_0.string_19);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/machineid", struct3_0.int_11);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/font_customized", struct3_0.string_16);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/font_embedded", struct3_0.string_17);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/font_subsetting", struct3_0.int_12);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/editor", struct3_0.string_0);
			if (struct3_0.string_22 != "")
			{
				Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/outputfolder", struct3_0.string_22);
			}
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/cssoverwrite", struct3_0.string_20);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/forcetextcover", struct3_0.string_1);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/addspace", struct3_0.string_13);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/addspacecount", struct3_0.int_8);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/savecss", struct3_0.string_21);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/simple_reg_leadingspace", struct3_0.int_0);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/simple_reg_p1", struct3_0.string_2);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/simple_reg_p2", struct3_0.int_1);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/simple_reg_p3", struct3_0.string_3);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/simple_reg_ext", struct3_0.string_4);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/RecentOptions/full_reg", struct3_0.string_5);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/mobistrip", struct2_0.int_10);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/mobiperiodical", struct2_0.int_11);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/mobiforceen", struct2_0.int_12);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/mobisync", struct2_0.int_13);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/asinstyle", struct2_0.int_14);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/mobiasin", struct2_0.string_2);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/kindlegenexe", struct2_0.string_3);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/kindlegencompress", struct2_0.int_15);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/mobiformat", struct2_0.int_16);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/kindlegenoption", struct2_0.string_4);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/silentmode", struct2_0.int_0);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/enable_htmlrawtag", struct2_0.int_1);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/htmlrawtag", struct2_0.string_0);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/enable_tempdir", struct2_0.int_2);
			Class34.smethod_48(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/tempdir", struct2_0.string_1);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/flowsize", struct2_0.int_3);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/screenwidth", struct2_0.int_4);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/screenheight", struct2_0.int_5);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/tocspace", struct2_0.int_6);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/outputtosrc", struct2_0.int_7);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/emptychapterstyle", struct2_0.int_8);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/outputformat", struct2_0.int_9);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/AdvancedOptions/alwaysontop", struct2_0.int_17);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/posx1", struct3_0.int_14);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/posy1", struct3_0.int_15);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/posx2", struct3_0.int_16);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/posy2", struct3_0.int_17);
			Class22.smethod_0(ref xmlDocument, "/EasyPubConfig/RecentOptions/automark", struct3_0.int_18);
			xmlDocument.Save(text);
			xmlDocument = null;
			return true;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00031FE4 File Offset: 0x000301E4
		static void smethod_26(Form1 form1_0)
		{
			if (!Directory.Exists(Path.GetDirectoryName(form1_0.struct1_0.string_4)))
			{
				DialogResult dialogResult = Class34.smethod_27(form1_0, string.Format("输出文件路径不存在!\n{0}\n点击”确定“创建此目录并继续\n点击”取消“返回并重新选择输出目录", Path.GetDirectoryName(form1_0.struct1_0.string_4)), "EasyPub", MessageBoxButtons.OKCancel);
				if (dialogResult != DialogResult.OK)
				{
					return;
				}
				Directory.CreateDirectory(Path.GetDirectoryName(form1_0.struct1_0.string_4));
			}
			if (Class9.smethod_0(form1_0.textBox_0.Text, form1_0.comboBox_27.Text))
			{
				form1_0.stopwatch_0.Stop();
				if (!form1_0.checkBox_14.Checked)
				{
					form1_0.struct3_0.string_22 = Path.GetDirectoryName(form1_0.comboBox_27.Text);
				}
				Class34.smethod_118(form1_0, string.Format("成功创建 {0}，用时{1}毫秒", form1_0.comboBox_27.Text, form1_0.stopwatch_0.ElapsedMilliseconds), "normal");
				if (!form1_0.checkBox_6.Checked)
				{
					DialogResult dialogResult2 = Class34.smethod_27(form1_0, "成功创建文件\n点击\"确定\"在文件管理器中定位文件\n点击\"取消\"返回程序", "EasyPub", MessageBoxButtons.OKCancel);
					if (dialogResult2 == DialogResult.OK)
					{
						try
						{
							Process.Start("explorer.exe", "/select," + form1_0.comboBox_27.Text);
						}
						catch (Exception ex)
						{
							Class34.smethod_118(form1_0, ex.Message, "error");
						}
					}
				}
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000085C5 File Offset: 0x000067C5
		static DialogResult smethod_27(IWin32Window iwin32Window_0, string string_0, string string_1, MessageBoxButtons messageBoxButtons_0)
		{
			Class8.iwin32Window_0 = iwin32Window_0;
			Class34.smethod_178();
			return MessageBox.Show(iwin32Window_0, string_0, string_1, messageBoxButtons_0);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00032138 File Offset: 0x00030338
		static string smethod_28(string string_0, Class3 class3_0, ZipFile zipFile_0)
		{
			if (class3_0.int_1 == 2)
			{
				return class3_0.string_0;
			}
			string text = "";
			MemoryStream memoryStream = new MemoryStream();
			zipFile_0[string_0].Extract(memoryStream);
			StreamReader streamReader = new StreamReader(memoryStream, Class34.smethod_37(memoryStream));
			memoryStream.Seek(0L, SeekOrigin.Begin);
			text = streamReader.ReadToEnd();
			Regex regex = new Regex("/\\*.*?\\*/", RegexOptions.Singleline);
			text = regex.Replace(text, "");
			regex = new Regex("(?i)url\\s*\\(\\s*[\"']?res:///.*?[\"']?\\s*\\)[,;]", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			text = regex.Replace(text, "");
			Trace.WriteLine(text);
			regex = new Regex("^\\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			text = regex.Replace(text, "");
			Trace.WriteLine(text);
			regex = new Regex(",\\s*}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			text = regex.Replace(text, ";}");
			Trace.WriteLine(text);
			regex = new Regex("src:\\s*}", RegexOptions.Singleline);
			text = regex.Replace(text, "}");
			List<string> list = new List<string>();
			string text2 = "";
			string text3 = "@font-face\\s*{\\s*font-family:\\s*['\"]?(.*?)['\"]?\\s*;";
			foreach (object obj in Regex.Matches(text, text3, RegexOptions.IgnoreCase | RegexOptions.Singleline))
			{
				Match match = (Match)obj;
				if (!list.Contains(match.Groups[1].Value))
				{
					text2 = text2 + "|" + match.Groups[1].Value;
					list.Add(match.Groups[1].Value);
				}
			}
			if (text2 != "")
			{
				text2 = text2.Substring(1, text2.Length - 1);
			}
			regex = new Regex("(font-family:.*?['\"]?(fontname)['\"]?[^;]*?;)".Replace("fontname", text2), RegexOptions.IgnoreCase | RegexOptions.Multiline);
			foreach (object obj2 in regex.Matches(text))
			{
				Match match2 = (Match)obj2;
				string value = match2.Groups[1].Value;
				if (value.Contains(","))
				{
					new List<string>();
					regex = new Regex("font-family\\s*:", RegexOptions.IgnoreCase);
					string text4 = regex.Replace(value, "");
					string text5 = "";
					foreach (string text6 in text4.Split(new char[] { ',' }))
					{
						text5 = text6.Trim().Trim(new char[] { ',', ';', '\'', '"' }).Trim();
						if (list.Contains(text5))
						{
							break;
						}
					}
					if (list.Contains(text5))
					{
						string text7 = "font-family: \"fontname\";".Replace("fontname", text5).PadRight(value.Length, ' ');
						text = text.Substring(0, match2.Index) + text7 + text.Substring(match2.Index + value.Length, text.Length - match2.Index - value.Length);
					}
				}
			}
			if (class3_0.int_1 == 1)
			{
				text = text + "\r\n" + class3_0.string_0;
			}
			return text;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000324CC File Offset: 0x000306CC
		static byte[] smethod_29(byte[] byte_0, Class13 class13_0, byte[] byte_1)
		{
			byte[] array = new byte[byte_0.Length + byte_1.Length];
			Buffer.BlockCopy(byte_0, 0, array, 0, byte_0.Length);
			Buffer.BlockCopy(byte_1, 0, array, byte_0.Length, byte_1.Length);
			return array;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00032504 File Offset: 0x00030704
		static void smethod_30(Form1 form1_0)
		{
			form1_0.struct1_0.string_6 = form1_0.textBox_0.Text;
			form1_0.struct1_0.string_5 = form1_0.textBox_2.Text.Trim();
			if (form1_0.struct1_0.string_5 == "")
			{
				form1_0.struct1_0.string_5 = "Unknown Title";
			}
			form1_0.struct1_0.string_0 = form1_0.textBox_1.Text.Trim();
			if (form1_0.struct1_0.string_0 == "")
			{
				form1_0.struct1_0.string_0 = "";
			}
			form1_0.struct1_0.string_1 = "easypub-" + Class34.smethod_67(form1_0.struct1_0.string_5 + "-" + form1_0.struct1_0.string_0).Substring(8, 8);
			form1_0.struct1_0.string_2 = "easypub-" + Class34.smethod_67(string.Concat(new string[]
			{
				form1_0.struct1_0.string_5,
				"-",
				form1_0.struct1_0.string_0,
				"-",
				DateTime.Now.ToUniversalTime().ToString("o")
			})).Substring(8, 8);
			form1_0.struct1_0.string_4 = form1_0.comboBox_27.Text;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0003267C File Offset: 0x0003087C
		static void smethod_31(Class10 class10_0)
		{
			while (Class34.smethod_145(Class34.smethod_88(class10_0)))
			{
				char c = Class34.smethod_88(class10_0);
				Class34.smethod_87(class10_0);
				if (c == '\n')
				{
					return;
				}
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000085DB File Offset: 0x000067DB
		static void smethod_32(Form2 form2_0)
		{
			Class34.smethod_140(form2_0, global::System.Drawing.Color.Firebrick);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000085E8 File Offset: 0x000067E8
		static void smethod_33(Form1 form1_0)
		{
			if (form1_0.bool_1)
			{
				form1_0.method_20();
				return;
			}
			Class34.smethod_177(form1_0);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000326B0 File Offset: 0x000308B0
		static void smethod_34(Form1 form1_0, string string_0)
		{
			Form2 form = new Form2(string_0, form1_0.list_1, form1_0.bool_0, form1_0.struct3_0.int_18);
			form.Location = new Point(form1_0.struct3_0.int_16, form1_0.struct3_0.int_17);
			form.StartPosition = FormStartPosition.Manual;
			form.ShowInTaskbar = false;
			form.ShowDialog(form1_0);
			if (form1_0.bool_0)
			{
				form1_0.list_1 = form.list_1;
			}
			form1_0.struct3_0.int_16 = form.Location.X;
			form1_0.struct3_0.int_17 = form.Location.Y;
			form1_0.struct3_0.int_18 = form.int_0;
			form.Close();
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00032770 File Offset: 0x00030970
		static int[] smethod_35(string string_0)
		{
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < string_0.Length; i++)
			{
				int num = char.ConvertToUtf32(string_0, i);
				if (num != 13 && num != 9 && num != 10)
				{
					hashSet.Add(num);
				}
				if (char.IsSurrogatePair(string_0, i))
				{
					i++;
				}
			}
			return hashSet.ToArray<int>();
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000085FF File Offset: 0x000067FF
		static bool smethod_36(Dictionary<int, string> dictionary_0, string string_0, Class13 class13_0)
		{
			return Class34.smethod_86(string_0, dictionary_0, class13_0, string_0);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000327C8 File Offset: 0x000309C8
		static Encoding smethod_37(MemoryStream memoryStream_0)
		{
			Encoding encoding = Encoding.Default;
			memoryStream_0.Seek(0L, SeekOrigin.Begin);
			try
			{
				byte[] array = new byte[5];
				if (memoryStream_0.Length <= 5L)
				{
					return Encoding.Default;
				}
				memoryStream_0.Read(array, 0, 4);
				string text = BitConverter.ToString(array).Replace("-", string.Empty);
				if (text.Substring(0, 6) == "EFBBBF")
				{
					encoding = Encoding.UTF8;
				}
				else if (text.Substring(0, 4) == "FEFF")
				{
					encoding = Encoding.BigEndianUnicode;
				}
				else if (text.Substring(0, 8) == "FFFE0000")
				{
					encoding = Encoding.UTF32;
				}
				else if (text.Substring(0, 4) == "FFFE")
				{
					encoding = Encoding.Unicode;
				}
				else
				{
					memoryStream_0.Seek(0L, SeekOrigin.Begin);
					int num = (int)memoryStream_0.Length;
					byte[] array2 = new byte[num];
					int num2 = 0;
					while ((array2[num2] = (byte)memoryStream_0.ReadByte()) >= 0)
					{
						num2++;
						if (num2 >= num || (array2[num2 - 1] == 10 && num2 > 300000))
						{
							break;
						}
					}
					if (Class34.smethod_80(array2, 0, num2))
					{
						encoding = Encoding.UTF8;
					}
				}
			}
			catch (Exception)
			{
			}
			return encoding;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0003293C File Offset: 0x00030B3C
		static Rectangle smethod_38(ListViewItem listViewItem_0, int int_0, Class18 class18_0)
		{
			int[] array = class18_0.method_0();
			Rectangle empty = Rectangle.Empty;
			if (int_0 >= array.Length)
			{
				throw new IndexOutOfRangeException("SubItem " + int_0 + " out of range");
			}
			if (listViewItem_0 == null)
			{
				throw new ArgumentNullException("Item");
			}
			Rectangle bounds = listViewItem_0.GetBounds(ItemBoundsPortion.Entire);
			int num = bounds.Left;
			int i;
			for (i = 0; i < array.Length; i++)
			{
				ColumnHeader columnHeader = class18_0.Columns[array[i]];
				if (columnHeader.Index == int_0)
				{
					break;
				}
				num += columnHeader.Width;
			}
			empty = new Rectangle(num, bounds.Top, class18_0.Columns[array[i]].Width, bounds.Height);
			return empty;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000329F8 File Offset: 0x00030BF8
		static void smethod_39(Form1 form1_0, string string_0)
		{
			FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
			Image image = Image.FromStream(fileStream);
			fileStream.Close();
			form1_0.pictureBox_0.Image = image;
			form1_0.struct1_0.string_3 = string_0;
			form1_0.checkBox_3.Enabled = false;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000860A File Offset: 0x0000680A
		static int smethod_40(Class27.Class29 class29_0)
		{
			return class29_0.int_1 - class29_0.int_0 + (class29_0.int_2 >> 3);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00032A40 File Offset: 0x00030C40
		static int smethod_41(int int_0, byte[] byte_0, Class27.Class28 class28_0, int int_1)
		{
			int num = 0;
			for (;;)
			{
				if (class28_0.int_4 != 11)
				{
					int num2 = Class34.smethod_69(int_1, int_0, class28_0.class30_0, byte_0);
					int_1 += num2;
					num += num2;
					int_0 -= num2;
					if (int_0 == 0)
					{
						return num;
					}
				}
				if (!Class34.smethod_97(class28_0))
				{
					if (class28_0.class30_0.int_1 <= 0)
					{
						break;
					}
					if (class28_0.int_4 == 11)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00008622 File Offset: 0x00006822
		static bool smethod_42(Form2 form2_0, ListViewItem listViewItem_0)
		{
			return (listViewItem_0.Font.Style | FontStyle.Strikeout) == listViewItem_0.Font.Style;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00008641 File Offset: 0x00006841
		static void smethod_43(Class18 class18_0, bool bool_0)
		{
			class18_0.bool_0 = bool_0;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00032AA4 File Offset: 0x00030CA4
		static void smethod_44(Form0 form0_0)
		{
			form0_0.icontainer_0 = new Container();
			form0_0.label_0 = new Label();
			form0_0.timer_0 = new global::System.Windows.Forms.Timer(form0_0.icontainer_0);
			form0_0.SuspendLayout();
			form0_0.label_0.AutoSize = true;
			form0_0.label_0.FlatStyle = FlatStyle.Flat;
			form0_0.label_0.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			form0_0.label_0.Location = new Point(162, 26);
			form0_0.label_0.Margin = new Padding(4, 0, 4, 0);
			form0_0.label_0.Name = "label1";
			form0_0.label_0.Size = new Size(0, 18);
			form0_0.label_0.TabIndex = 0;
			form0_0.label_0.TextAlign = ContentAlignment.MiddleCenter;
			form0_0.timer_0.Tick += form0_0.method_1;
			form0_0.AutoScaleDimensions = new SizeF(9f, 16f);
			form0_0.AutoScaleMode = AutoScaleMode.Font;
			form0_0.ClientSize = new Size(180, 67);
			form0_0.Controls.Add(form0_0.label_0);
			form0_0.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			form0_0.FormBorderStyle = FormBorderStyle.None;
			form0_0.Margin = new Padding(4);
			form0_0.Name = "PleaseWaitDlg";
			form0_0.StartPosition = FormStartPosition.CenterParent;
			form0_0.Text = "PlsWait";
			form0_0.TransparencyKey = global::System.Drawing.Color.White;
			form0_0.FormClosing += form0_0.method_3;
			form0_0.Load += form0_0.method_0;
			form0_0.Shown += form0_0.method_4;
			form0_0.Paint += form0_0.method_2;
			form0_0.ResumeLayout(false);
			form0_0.PerformLayout();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000864A File Offset: 0x0000684A
		static DialogResult smethod_45(MessageBoxButtons messageBoxButtons_0, string string_0, string string_1)
		{
			Class34.smethod_178();
			return MessageBox.Show(string_0, string_1, messageBoxButtons_0);
		}

		// Token: 0x0600061C RID: 1564
		[DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern IntPtr SendMessage(IntPtr intptr_0, int int_0, IntPtr intptr_1, IntPtr intptr_2);

		// Token: 0x0600061D RID: 1565 RVA: 0x00032C78 File Offset: 0x00030E78
		static int smethod_46(ref string string_0, string string_1, string string_2, string string_3)
		{
			GlyphTypeface glyphTypeface = new GlyphTypeface(new Uri(string_3));
			List<ushort> list = new List<ushort>();
			ICollection<int> collection = Class34.smethod_35(string_1);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int num in collection)
			{
				short num2 = 0;
				glyphTypeface.CharacterToGlyphMap.TryGetValue(num, out num2);
				if (num2 != 0)
				{
					list.Add((ushort)num2);
				}
				else
				{
					stringBuilder.Append(char.ConvertFromUtf32(num));
				}
			}
			if (list.Count == 0)
			{
				ushort num3;
				glyphTypeface.CharacterToGlyphMap.TryGetValue(65, out num3);
				if (num3 != 0)
				{
					list.Add(num3);
				}
			}
			byte[] array = glyphTypeface.ComputeSubset(list);
			FileStream fileStream = new FileStream(string_2, FileMode.Create);
			fileStream.Write(array, 0, array.Length);
			fileStream.Close();
			if (string_0 != "SKIP" && stringBuilder.Length > 0)
			{
				string_0 = stringBuilder.ToString();
			}
			return collection.Count;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00008659 File Offset: 0x00006859
		static void smethod_47(Class27.Class29 class29_0)
		{
			class29_0.uint_0 >>= class29_0.int_2 & 7;
			class29_0.int_2 &= -8;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00032D80 File Offset: 0x00030F80
		static void smethod_48(ref XmlDocument xmlDocument_0, string string_0, string string_1)
		{
			try
			{
				xmlDocument_0.SelectSingleNode(string_0).InnerText = string_1;
			}
			catch (Exception)
			{
				int num = string_0.LastIndexOf('/');
				string text = string_0.Substring(0, num);
				string text2 = string_0.Substring(num + 1, string_0.Length - num - 1);
				XmlNode xmlNode = xmlDocument_0.SelectSingleNode(text);
				XmlNode xmlNode2 = xmlDocument_0.CreateElement(text2);
				xmlNode2.InnerText = string_1;
				xmlNode.AppendChild(xmlNode2);
			}
		}

		// Token: 0x06000620 RID: 1568
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		static extern IntPtr SendMessage_1(IntPtr intptr_0, int int_0, IntPtr intptr_1, IntPtr intptr_2);

		// Token: 0x06000621 RID: 1569 RVA: 0x00008682 File Offset: 0x00006882
		static void smethod_49(EventArgs1 eventArgs1_0, Class18 class18_0)
		{
			if (class18_0.delegate2_0 != null)
			{
				class18_0.delegate2_0(class18_0, eventArgs1_0);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00032DFC File Offset: 0x00030FFC
		static ICryptoTransform smethod_50(bool bool_0, byte[] byte_0, byte[] byte_1)
		{
			ICryptoTransform cryptoTransform;
			using (DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider())
			{
				cryptoTransform = (bool_0 ? descryptoServiceProvider.CreateDecryptor(byte_1, byte_0) : descryptoServiceProvider.CreateEncryptor(byte_1, byte_0));
			}
			return cryptoTransform;
		}

		// Token: 0x06000623 RID: 1571
		[DllImport("USER32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		static extern int AppendMenuW(IntPtr intptr_0, int int_0, int int_1, string string_0);

		// Token: 0x06000624 RID: 1572 RVA: 0x00032E44 File Offset: 0x00031044
		static bool smethod_51(string string_0, string string_1)
		{
			byte[] array = File.ReadAllBytes(string_0);
			string text = Encoding.ASCII.GetString(array, 60, 8);
			if (text != "BOOKMOBI")
			{
				return false;
			}
			short num = (short)Class34.smethod_24(array, 76);
			int num2 = (int)Class34.smethod_83(array, 78);
			Class34.smethod_83(array, 82);
			int num3 = (int)Class34.smethod_83(array, 86);
			Class34.smethod_83(array, 90);
			byte[] array2 = Class34.smethod_23(num3, array, num2);
			int num4 = (int)Class34.smethod_83(array2, 224);
			int num5 = (int)Class34.smethod_83(array2, 228);
			if (num4 == -1 || num5 == 0)
			{
				return false;
			}
			int num6 = num4 + num5;
			int num7 = (int)Class34.smethod_83(array, 78 + num4 * 8);
			int num8 = (int)Class34.smethod_83(array, 78 + num4 * 8 + 4);
			int num9 = (int)Class34.smethod_83(array, 78 + num6 * 8);
			num8 = (int)Class34.smethod_83(array, 78 + num6 * 8 + 4);
			int num10 = num9 - num7;
			text = Encoding.ASCII.GetString(array, num7, 4);
			if (text != "SRCS")
			{
				return false;
			}
			byte[] array3 = Class34.smethod_116(array, 68, Class34.smethod_79((uint)(((int)num - num5) * 2 + 1)));
			array3 = Class34.smethod_106(72, 4, array, array3);
			array3 = Class34.smethod_159(array3, Class34.smethod_13((ushort)((int)num - num5)));
			int num11 = 8 * num5;
			int num12 = 0;
			while ((long)num12 < (long)((ulong)num4))
			{
				int num13 = (int)Class34.smethod_83(array, 78 + num12 * 8);
				num8 = (int)Class34.smethod_83(array, 78 + num12 * 8 + 4);
				num13 -= num11;
				array3 = Class34.smethod_159(array3, Class34.smethod_79((uint)num13));
				array3 = Class34.smethod_159(array3, Class34.smethod_79((uint)num8));
				num12++;
			}
			num11 += num10;
			for (int i = num4 + num5; i < (int)num; i++)
			{
				int num14 = (int)Class34.smethod_83(array, 78 + i * 8);
				num14 -= num11;
				num8 = 2 * (i - num5);
				array3 = Class34.smethod_159(array3, Class34.smethod_79((uint)num14));
				array3 = Class34.smethod_159(array3, Class34.smethod_79((uint)num8));
			}
			int num15 = (int)Class34.smethod_83(array3, 78);
			num8 = (int)Class34.smethod_83(array3, 82);
			byte[] array4 = new byte[(ulong)num15 - (ulong)((long)array3.Length)];
			array3 = Class34.smethod_159(array3, array4);
			array3 = Class34.smethod_106(num2, num7 - num2, array, array3);
			array3 = Class34.smethod_106(num7 + num10, (int)((long)array.Length - (long)((ulong)num7) - (long)((ulong)num10)), array, array3);
			num2 = (int)Class34.smethod_83(array3, 78);
			Class34.smethod_83(array3, 82);
			num3 = (int)Class34.smethod_83(array3, 86);
			Class34.smethod_83(array3, 90);
			array2 = Class34.smethod_23(num3, array3, num2);
			array4 = Class34.smethod_23(224, array2, 0);
			array4 = Class34.smethod_159(array4, Class34.smethod_79(uint.MaxValue));
			array4 = Class34.smethod_159(array4, Class34.smethod_79(0U));
			array4 = Class34.smethod_159(array4, Class34.smethod_23(array2.Length, array2, 232));
			array2 = Class34.smethod_142((uint)num5, (uint)num4, array4);
			array4 = Class34.smethod_23(num2, array3, 0);
			array4 = Class34.smethod_159(array4, array2);
			array4 = Class34.smethod_159(array4, Class34.smethod_23(array3.Length, array3, num3));
			File.WriteAllBytes(string_1, array4);
			return true;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00033154 File Offset: 0x00031354
		static Tuple<uint, uint, uint> smethod_52(byte[] byte_0)
		{
			int num = Class5.Class6.int_6 + (int)Class34.smethod_165(byte_0, Class5.Class6.int_7, "L");
			int num2 = (int)Class34.smethod_165(byte_0, num + 4, "L");
			int num3 = (int)Class34.smethod_165(byte_0, num + 8, "L");
			return new Tuple<uint, uint, uint>((uint)num, (uint)num2, (uint)num3);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00008699 File Offset: 0x00006899
		static bool smethod_53(Class18 class18_0)
		{
			return class18_0.bool_1;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000331A0 File Offset: 0x000313A0
		static void smethod_54(IntPtr intptr_0)
		{
			Rectangle rectangle = new Rectangle(0, 0, 0, 0);
			Class34.GetWindowRect_1(intptr_0, ref rectangle);
			int num = rectangle.Width - rectangle.X;
			int num2 = rectangle.Height - rectangle.Y;
			Rectangle rectangle2 = new Rectangle(0, 0, 0, 0);
			Class34.GetWindowRect_1(Class8.iwin32Window_0.Handle, ref rectangle2);
			Point point = new Point(0, 0);
			point.X = rectangle2.X + (rectangle2.Width - rectangle2.X) / 2;
			point.Y = rectangle2.Y + (rectangle2.Height - rectangle2.Y) / 2;
			Point point2 = new Point(0, 0);
			point2.X = point.X - num / 2;
			point2.Y = point.Y - num2 / 2;
			point2.X = ((point2.X < 0) ? 0 : point2.X);
			point2.Y = ((point2.Y < 0) ? 0 : point2.Y);
			Class34.MoveWindow(intptr_0, point2.X, point2.Y, num, num2, false);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000332CC File Offset: 0x000314CC
		static void smethod_55(Form1 form1_0)
		{
			form1_0.textBox_19.Text = "";
			form1_0.textBox_17.Text = "";
			form1_0.textBox_18.Text = "";
			form1_0.textBox_20.Text = "";
			form1_0.comboBox_22.Text = "";
			form1_0.textBox_20.Text = "";
			form1_0.comboBox_21.Text = "zh-CN";
			form1_0.dateTimePicker_0.Value = DateTime.Now;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000086A1 File Offset: 0x000068A1
		static bool smethod_56(Class18 class18_0)
		{
			return class18_0.bool_0;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0003335C File Offset: 0x0003155C
		static void smethod_57(int int_0, int int_1, Class18 class18_0, ListViewItem listViewItem_0, int int_2, TextBox textBox_0)
		{
			Class34.smethod_99(new EventArgs0(listViewItem_0, int_1), class18_0);
			Rectangle rectangle = Class34.smethod_38(listViewItem_0, int_1, class18_0);
			if (rectangle.X < 0)
			{
				rectangle.Width += rectangle.X;
				rectangle.X = 0;
			}
			if (rectangle.X + rectangle.Width > class18_0.Width)
			{
				rectangle.Width = class18_0.Width - rectangle.Left;
			}
			rectangle.Offset(class18_0.Left, class18_0.Top);
			Point point = new Point(0, 0);
			Point point2 = class18_0.Parent.PointToScreen(point);
			Point point3 = textBox_0.Parent.PointToScreen(point);
			rectangle.Offset(point2.X - point3.X + int_0, point2.Y - point3.Y + int_2);
			textBox_0.Bounds = rectangle;
			textBox_0.Text = listViewItem_0.SubItems[int_1].Text;
			textBox_0.Visible = true;
			textBox_0.BringToFront();
			textBox_0.Focus();
			textBox_0.SelectionStart = textBox_0.Text.Length;
			class18_0.control_0 = textBox_0;
			class18_0.control_0.Leave += class18_0.method_1;
			class18_0.control_0.KeyPress += class18_0.method_2;
			class18_0.listViewItem_0 = listViewItem_0;
			class18_0.int_0 = int_1;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000334C4 File Offset: 0x000316C4
		static string smethod_58(XmlDocument xmlDocument_0, string string_0, string string_1)
		{
			try
			{
				string text = xmlDocument_0.SelectSingleNode(string_0).InnerText.Trim();
				if (text == "")
				{
					text = string_1;
				}
				return text;
			}
			catch (Exception)
			{
			}
			return string_1;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0003350C File Offset: 0x0003170C
		static void smethod_59(Class18 class18_0, bool bool_0)
		{
			if (class18_0.control_0 == null)
			{
				return;
			}
			EventArgs1 eventArgs = new EventArgs1(class18_0.listViewItem_0, class18_0.int_0, bool_0 ? class18_0.control_0.Text : class18_0.listViewItem_0.SubItems[class18_0.int_0].Text, !bool_0);
			Class34.smethod_49(eventArgs, class18_0);
			class18_0.listViewItem_0.SubItems[class18_0.int_0].Text = eventArgs.string_0;
			class18_0.listViewItem_0.Tag = eventArgs.string_0;
			class18_0.control_0.Leave -= class18_0.method_1;
			class18_0.control_0.KeyPress -= class18_0.method_2;
			class18_0.control_0.Visible = false;
			class18_0.control_0 = null;
			class18_0.listViewItem_0 = null;
			class18_0.int_0 = -1;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000335EC File Offset: 0x000317EC
		static int smethod_60(XmlDocument xmlDocument_0, string string_0, int int_0)
		{
			try
			{
				return int.Parse(xmlDocument_0.SelectSingleNode(string_0).InnerText.Trim());
			}
			catch (Exception)
			{
			}
			return int_0;
		}

		// Token: 0x0600062E RID: 1582
		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(int int_0, Class8.Delegate0 delegate0_0, IntPtr intptr_0, int int_1);

		// Token: 0x0600062F RID: 1583 RVA: 0x0003362C File Offset: 0x0003182C
		static IntPtr smethod_61(ToolTip toolTip_0)
		{
			Type typeFromHandle = typeof(ToolTip);
			object obj = typeFromHandle.InvokeMember("Handle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty, null, toolTip_0, null);
			return (IntPtr)obj;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00033660 File Offset: 0x00031860
		static uint smethod_62(Class13 class13_0, byte[] byte_0, int int_0)
		{
			uint num = BitConverter.ToUInt32(byte_0, int_0);
			return ((num & 255U) << 24) + ((num & 65280U) << 8) + ((num & 16711680U) >> 8) + ((num & 4278190080U) >> 24);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000336A0 File Offset: 0x000318A0
		static Class27.Class31 smethod_63(Class27.Class32 class32_0)
		{
			byte[] array = new byte[class32_0.int_3];
			Array.Copy(class32_0.byte_1, 0, array, 0, class32_0.int_3);
			return new Class27.Class31(array);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000086A9 File Offset: 0x000068A9
		static int smethod_64(Class27.Stream0 stream0_0)
		{
			return Class34.smethod_151(stream0_0) | (Class34.smethod_151(stream0_0) << 16);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000086BB File Offset: 0x000068BB
		static int smethod_65(EventArgs0 eventArgs0_0)
		{
			return eventArgs0_0.int_0;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000336D4 File Offset: 0x000318D4
		static byte[] smethod_66(byte[] byte_0)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (callingAssembly != executingAssembly && !Class34.smethod_171(executingAssembly, callingAssembly))
			{
				return null;
			}
			Class27.Stream0 stream = new Class27.Stream0(byte_0);
			byte[] array = new byte[0];
			int num = Class34.smethod_64(stream);
			if (num == 67324752)
			{
				short num2 = (short)Class34.smethod_151(stream);
				int num3 = Class34.smethod_151(stream);
				int num4 = Class34.smethod_151(stream);
				if (num == 67324752 && num2 == 20 && num3 == 0)
				{
					if (num4 == 8)
					{
						Class34.smethod_64(stream);
						Class34.smethod_64(stream);
						Class34.smethod_64(stream);
						int num5 = Class34.smethod_64(stream);
						int num6 = Class34.smethod_151(stream);
						int num7 = Class34.smethod_151(stream);
						if (num6 > 0)
						{
							byte[] array2 = new byte[num6];
							stream.Read(array2, 0, num6);
						}
						if (num7 > 0)
						{
							byte[] array3 = new byte[num7];
							stream.Read(array3, 0, num7);
						}
						byte[] array4 = new byte[stream.Length - stream.Position];
						stream.Read(array4, 0, array4.Length);
						Class27.Class28 @class = new Class27.Class28(array4);
						array = new byte[num5];
						Class34.smethod_41(array.Length, array, @class, 0);
						goto IL_0279;
					}
				}
				throw new FormatException("Wrong Header Signature");
			}
			int num8 = num >> 24;
			num -= num8 << 24;
			if (num == 8223355)
			{
				if (num8 == 1)
				{
					int num9 = Class34.smethod_64(stream);
					array = new byte[num9];
					int num11;
					for (int i = 0; i < num9; i += num11)
					{
						int num10 = Class34.smethod_64(stream);
						num11 = Class34.smethod_64(stream);
						byte[] array5 = new byte[num10];
						stream.Read(array5, 0, array5.Length);
						Class27.Class28 class2 = new Class27.Class28(array5);
						Class34.smethod_41(num11, array, class2, i);
					}
				}
				if (num8 == 2)
				{
					byte[] array6 = new byte[] { 69, 95, 4, 167, 102, 155, 240, 201 };
					byte[] array7 = new byte[] { 94, 67, 130, 234, 105, 239, 189, 44 };
					using (ICryptoTransform cryptoTransform = Class34.smethod_50(true, array7, array6))
					{
						byte[] array8 = cryptoTransform.TransformFinalBlock(byte_0, 4, byte_0.Length - 4);
						array = Class34.smethod_66(array8);
					}
				}
				if (num8 != 3)
				{
					goto IL_0279;
				}
				byte[] array9 = new byte[]
				{
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1
				};
				byte[] array10 = new byte[]
				{
					2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
					2, 2, 2, 2, 2, 2
				};
				using (ICryptoTransform cryptoTransform2 = Class34.smethod_139(array10, true, array9))
				{
					byte[] array11 = cryptoTransform2.TransformFinalBlock(byte_0, 4, byte_0.Length - 4);
					array = Class34.smethod_66(array11);
					goto IL_0279;
				}
			}
			throw new FormatException("Unknown Header");
			IL_0279:
			stream.Close();
			stream = null;
			return array;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00033980 File Offset: 0x00031B80
		static string smethod_67(string string_0)
		{
			MD5 md = MD5.Create();
			byte[] array = md.ComputeHash(Encoding.Default.GetBytes(string_0));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000339D8 File Offset: 0x00031BD8
		static IntPtr smethod_68(int int_0, IntPtr intptr_0, IntPtr intptr_1)
		{
			if (int_0 < 0)
			{
				return Class34.CallNextHookEx(Class8.intptr_0, int_0, intptr_0, intptr_1);
			}
			Class8.Struct5 @struct = (Class8.Struct5)Marshal.PtrToStructure(intptr_1, typeof(Class8.Struct5));
			IntPtr intptr_2 = Class8.intptr_0;
			if (@struct.uint_0 == 5U)
			{
				try
				{
					Class34.smethod_54(@struct.intptr_3);
				}
				finally
				{
					Class34.UnhookWindowsHookEx(Class8.intptr_0);
					Class8.intptr_0 = IntPtr.Zero;
				}
			}
			return Class34.CallNextHookEx(intptr_2, int_0, intptr_0, intptr_1);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00033A5C File Offset: 0x00031C5C
		static int smethod_69(int int_0, int int_1, Class27.Class30 class30_0, byte[] byte_0)
		{
			int num = class30_0.int_0;
			if (int_1 > class30_0.int_1)
			{
				int_1 = class30_0.int_1;
			}
			else
			{
				num = (class30_0.int_0 - class30_0.int_1 + int_1) & 32767;
			}
			int num2 = int_1;
			int num3 = int_1 - num;
			if (num3 > 0)
			{
				Array.Copy(class30_0.byte_0, 32768 - num3, byte_0, int_0, num3);
				int_0 += num3;
				int_1 = num;
			}
			Array.Copy(class30_0.byte_0, num - int_1, byte_0, int_0, int_1);
			class30_0.int_1 -= num2;
			if (class30_0.int_1 < 0)
			{
				throw new InvalidOperationException();
			}
			return num2;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00033AF0 File Offset: 0x00031CF0
		static int smethod_70(Form1 form1_0, string string_0)
		{
			form1_0.string_3 = form1_0.textBox_0.Text;
			if (File.Exists(form1_0.string_3))
			{
				Class34.smethod_117(form1_0.string_3, form1_0);
				if (string_0 != "")
				{
					int num;
					try
					{
						Regex regex = new Regex(string_0);
						for (int i = 0; i < Form1.list_0.Count - 1; i++)
						{
							if (regex.IsMatch(Form1.list_0[i]))
							{
								return 1;
							}
						}
						return 0;
					}
					catch (Exception ex)
					{
						Class34.smethod_118(form1_0, "表达式出错: " + ex.Message, "warning");
						num = 0;
					}
					return num;
				}
				return 0;
			}
			Class34.smethod_153(form1_0, "请先选择文件！", "EasyPub");
			return 0;
		}

		// Token: 0x06000639 RID: 1593
		public static int smethod_71(string string_0, string string_1, StreamWriter streamWriter_0, Form1 form1_0)
		{
			string text = string_1.Trim();
			string text0 = null;
			if (text != "")
			{
				if (text.StartsWith("<img ", StringComparison.OrdinalIgnoreCase))
				{
					Match match = Regex.Match(text, "src\\s*=\\s*[\"']([^\"']+)[\"']", RegexOptions.IgnoreCase);
					if (match.Success)
					{
						text0 = match.Groups[1].Value;
					}
				}
				else if (text.StartsWith("!["))
				{
					Match match2 = Regex.Match(text, "^!\\[[^\\]]*\\]\\(([^)]+)\\)$");
					if (match2.Success)
					{
						text0 = match2.Groups[1].Value;
					}
				}
				else if (text.StartsWith("http://") || text.StartsWith("https://"))
				{
					text0 = text;
				}
			}
			if (text0 != null && (text0.StartsWith("http://") || text0.StartsWith("https://")))
			{
				try
				{
					Uri uri = new Uri(text0);
					string text2 = uri.AbsolutePath.ToLower();
					if (text2.EndsWith(".jpg") || text2.EndsWith(".jpeg") || text2.EndsWith(".png") || text2.EndsWith(".gif") || text2.EndsWith(".webp") || text2.EndsWith(".bmp") || text2.EndsWith(".svg"))
					{
						string text3 = Path.GetFileName(uri.AbsolutePath);
						int num = text3.IndexOf('?');
						if (num >= 0)
						{
							text3 = text3.Substring(0, num);
						}
						string text4 = Path.Combine(form1_0.string_1, "OEBPS", "images");
						if (!Directory.Exists(text4))
						{
							Directory.CreateDirectory(text4);
						}
						string text5 = Path.Combine(text4, text3);
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text3);
						string extension = Path.GetExtension(text3);
						int num2 = 1;
						while (File.Exists(text5))
						{
							text3 = fileNameWithoutExtension + "_" + num2.ToString() + extension;
							text5 = Path.Combine(text4, text3);
							num2++;
						}
						using (WebClient webClient = new WebClient())
						{
							webClient.DownloadFile(text0, text5);
						}
						streamWriter_0.WriteLine("<div class=\"a\"><img src=\"images/" + HttpUtility.HtmlEncode(text3) + "\" alt=\"\" /></div>");
						return 1;
					}
				}
				catch
				{
				}
			}
			if (form1_0.checkBox_7.Checked && form1_0.textBox_11.Text != "")
			{
				string text6 = form1_0.textBox_11.Text;
				int length = text6.Length;
				if (string_1.Length >= text6.Length && string_1.Substring(0, length) == text6)
				{
					streamWriter_0.WriteLine(string_1.Substring(length));
					return 1;
				}
			}
			if (form1_0.checkBox_2.Checked)
			{
				string text7 = string_1.Trim();
				if (text7 != "")
				{
					if (form1_0.checkBox_4.Checked)
					{
						streamWriter_0.WriteLine("<p class=\"a\">" + string_0 + HttpUtility.HtmlEncode(text7) + "</p>");
					}
					else
					{
						streamWriter_0.WriteLine("<p class=\"a\">" + HttpUtility.HtmlEncode(string_1) + "</p>");
					}
					return 1;
				}
				return 0;
			}
			else
			{
				string text8 = string_1.Trim();
				if (form1_0.checkBox_4.Checked && string_0 != "")
				{
					streamWriter_0.WriteLine("<p class=\"a\">" + string_0 + HttpUtility.HtmlEncode(text8) + "</p>");
					return 1;
				}
				if (form1_0.checkBox_4.Checked && string_0 == "")
				{
					if (text8 != "")
					{
						streamWriter_0.WriteLine("<p class=\"a\">" + HttpUtility.HtmlEncode(text8) + "</p>");
					}
					else
					{
						streamWriter_0.WriteLine("<p class=\"a\">\u3000</p>");
					}
					return 1;
				}
				if (string_1.Trim() != "")
				{
					streamWriter_0.WriteLine("<p class=\"a\">" + HttpUtility.HtmlEncode(string_1) + "</p>");
				}
				else
				{
					streamWriter_0.WriteLine("<p class=\"a\">\u3000</p>");
				}
				return 1;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00033F28 File Offset: 0x00032128
		static string smethod_72([Out] Class10 class10_0, ref bool bool_0)
		{
			string text = string.Empty;
			bool_0 = false;
			if (Class34.smethod_88(class10_0) == '<')
			{
				Class34.smethod_87(class10_0);
				Class34.smethod_157(class10_0);
				int int_ = class10_0.int_0;
				if (Class34.smethod_88(class10_0) == '/')
				{
					Class34.smethod_87(class10_0);
				}
				while (!Class34.smethod_121(class10_0) && !Class34.smethod_145(Class34.smethod_88(class10_0)) && Class34.smethod_88(class10_0) != '/')
				{
					if (Class34.smethod_88(class10_0) == '>')
					{
						break;
					}
					Class34.smethod_87(class10_0);
				}
				text = class10_0.string_0.Substring(int_, class10_0.int_0 - int_).ToLower();
				while (!Class34.smethod_121(class10_0))
				{
					if (Class34.smethod_88(class10_0) == '>')
					{
						break;
					}
					if (Class34.smethod_88(class10_0) != '"')
					{
						if (Class34.smethod_88(class10_0) != '\'')
						{
							if (Class34.smethod_88(class10_0) == '/')
							{
								bool_0 = true;
							}
							Class34.smethod_87(class10_0);
							continue;
						}
					}
					Class34.smethod_134(class10_0);
				}
				Class34.smethod_87(class10_0);
			}
			return text;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0003400C File Offset: 0x0003220C
		static ColumnHeader smethod_73(Point point_0, ListView listView_0)
		{
			if (listView_0.View != View.Details)
			{
				return null;
			}
			IntPtr intPtr = Class34.SendMessage_3(listView_0.Handle, 4127, 0, 0);
			Class4.Struct4 @struct;
			Class34.GetWindowRect(intPtr, out @struct);
			if (point_0.X >= @struct.int_0 && point_0.X <= @struct.int_2 && point_0.Y >= @struct.int_1 && point_0.Y <= @struct.int_3)
			{
				int num = @struct.int_0;
				using (IEnumerator enumerator = listView_0.Columns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ColumnHeader columnHeader = (ColumnHeader)obj;
						num += columnHeader.Width;
						if (point_0.X < num)
						{
							return columnHeader;
						}
					}
					goto IL_00CD;
				}
				ColumnHeader columnHeader2;
				return columnHeader2;
			}
			IL_00CD:
			return null;
		}

		// Token: 0x0600063C RID: 1596
		[DllImport("user32.dll")]
		static extern int UnhookWindowsHookEx(IntPtr intptr_0);

		// Token: 0x0600063D RID: 1597 RVA: 0x000340F8 File Offset: 0x000322F8
		static string smethod_74(int int_0, Form1 form1_0)
		{
			string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			char[] array = new char[int_0];
			for (int i = 0; i < int_0; i++)
			{
				array[i] = text[form1_0.random_0.Next(text.Length)];
			}
			return new string(array);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000086C3 File Offset: 0x000068C3
		static bool smethod_75(string string_0, int int_0, Class21 class21_0)
		{
			return Class34.smethod_82(string_0, Enum0.const_0, class21_0, int_0);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00034140 File Offset: 0x00032340
		static byte[] smethod_76(byte[] byte_0, int int_0)
		{
			byte[] array = new byte[0];
			int num = (int)Class34.smethod_165(byte_0, Class5.Class6.int_1, "H");
			Tuple<int, int> tuple = Class34.smethod_81(byte_0, 0);
			int item = tuple.Item1;
			int item2 = tuple.Item2;
			tuple = Class34.smethod_81(byte_0, int_0);
			int item3 = tuple.Item1;
			int item4 = tuple.Item2;
			int num2 = item4 - item3;
			array = array.smethod_0(byte_0.smethod_6(0, Class5.Class6.int_4));
			for (int i = 0; i < int_0 + 1; i++)
			{
				int num3 = (int)byte_0.smethod_3(Class5.Class6.int_4 + i * 8);
				int num4 = (int)byte_0.smethod_3(Class5.Class6.int_4 + i * 8 + 4);
				array = array.smethod_0(((uint)num3).smethod_5());
				array = array.smethod_0(((uint)num4).smethod_5());
			}
			int num5 = int_0 + 1;
			while ((long)num5 < (long)((ulong)num))
			{
				int num6 = (int)byte_0.smethod_3(Class5.Class6.int_4 + num5 * 8);
				int num7 = (int)byte_0.smethod_3(Class5.Class6.int_4 + num5 * 8 + 4);
				num6 -= num2;
				array = array.smethod_0(((uint)num6).smethod_5());
				array = array.smethod_0(((uint)num7).smethod_5());
				num5++;
			}
			long num8 = (long)item - ((long)Class5.Class6.int_4 + (long)((ulong)(8 * num)));
			if (num8 > 0L)
			{
				byte[] array2 = Enumerable.Repeat<byte>(0, (int)num8).ToArray<byte>();
				array = array.smethod_0(array2);
			}
			array = array.smethod_0(byte_0.smethod_6(item, item3));
			return array.smethod_0(byte_0.smethod_6(item4, byte_0.Length));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000342B8 File Offset: 0x000324B8
		static void smethod_77()
		{
			try
			{
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					MemoryManager.memoryManager_0 = new MemoryManager();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000342F4 File Offset: 0x000324F4
		static string smethod_78(Form2 form2_0, string string_0)
		{
			int num = 160;
			if (string_0.Length < 165)
			{
				return string_0;
			}
			return string_0.Substring(0, num - 40) + "…………" + string_0.Substring(string_0.Length - 40);
		}

		// Token: 0x06000642 RID: 1602
		[DllImport("kernel32.dll")]
		static extern int GetCurrentThreadId();

		// Token: 0x06000643 RID: 1603 RVA: 0x00008AA0 File Offset: 0x00006CA0
		static byte[] smethod_79(uint uint_0)
		{
			byte[] bytes = BitConverter.GetBytes(uint_0);
			Array.Reverse(bytes, 0, bytes.Length);
			return bytes;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0003433C File Offset: 0x0003253C
		static bool smethod_80(byte[] byte_0, int int_0, int int_1)
		{
			string @string = Encoding.UTF8.GetString(byte_0, int_0, int_1);
			byte[] bytes = Encoding.UTF8.GetBytes(@string);
			if (int_1 != bytes.Length)
			{
				return false;
			}
			for (int i = 0; i < int_1; i++)
			{
				if (byte_0[i] != bytes[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00034384 File Offset: 0x00032584
		static Tuple<int, int> smethod_81(byte[] byte_0, int int_0)
		{
			int num = (int)Class34.smethod_165(byte_0, Class5.Class6.int_1, "H");
			int num2 = (int)Class34.smethod_165(byte_0, Class5.Class6.int_4 + int_0 * 8, "L");
			int num3;
			if ((long)int_0 == (long)((ulong)(num - 1)))
			{
				num3 = byte_0.Length;
			}
			else
			{
				num3 = (int)Class34.smethod_165(byte_0, Class5.Class6.int_4 + (int_0 + 1) * 8, "L");
			}
			return new Tuple<int, int>(num2, num3);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000086CE File Offset: 0x000068CE
		static bool smethod_82(string string_0, Enum0 enum0_0, Class21 class21_0, int int_0)
		{
			return Class34.AppendMenuW(class21_0.intptr_0, (int)enum0_0, int_0, string_0) == 0;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00008A40 File Offset: 0x00006C40
		static uint smethod_83(byte[] byte_0, int int_0)
		{
			uint num = BitConverter.ToUInt32(byte_0, int_0);
			return ((num & 255U) << 24) + ((num & 65280U) << 8) + ((num & 16711680U) >> 8) + ((num & 4278190080U) >> 24);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000343E4 File Offset: 0x000325E4
		static void smethod_84(string string_0, Class10.Class11 class11_0)
		{
			foreach (char c in string_0)
			{
				Class34.smethod_7(class11_0, c);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00034414 File Offset: 0x00032614
		static void smethod_85(int int_0, int int_1, byte[] byte_0, Class27.Class29 class29_0)
		{
			if (class29_0.int_0 < class29_0.int_1)
			{
				throw new InvalidOperationException();
			}
			int num = int_0 + int_1;
			if (0 <= int_0 && int_0 <= num && num <= byte_0.Length)
			{
				if ((int_1 & 1) != 0)
				{
					class29_0.uint_0 |= (uint)((uint)(byte_0[int_0++] & byte.MaxValue) << class29_0.int_2);
					class29_0.int_2 += 8;
				}
				class29_0.byte_0 = byte_0;
				class29_0.int_0 = int_0;
				class29_0.int_1 = num;
				return;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0003449C File Offset: 0x0003269C
		static bool smethod_86(string string_0, Dictionary<int, string> dictionary_0, Class13 class13_0, string string_1)
		{
			byte[] array = File.ReadAllBytes(string_0);
			byte[] array2 = class13_0.method_0(array, dictionary_0);
			if (array2.Length == 1)
			{
				return false;
			}
			array2 = class13_0.method_1(array2, dictionary_0);
			File.WriteAllBytes(string_1, array2);
			return true;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000086E1 File Offset: 0x000068E1
		static void smethod_87(Class10 class10_0)
		{
			class10_0.int_0 = Math.Min(class10_0.int_0 + 1, class10_0.string_0.Length);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00008701 File Offset: 0x00006901
		static char smethod_88(Class10 class10_0)
		{
			if (class10_0.int_0 >= class10_0.string_0.Length)
			{
				return '\0';
			}
			return class10_0.string_0[class10_0.int_0];
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000344D4 File Offset: 0x000326D4
		static void smethod_89(XmlNode xmlNode_0, Class3 class3_0, XmlNode xmlNode_1)
		{
			if (xmlNode_0.ChildNodes.Count == 0)
			{
				if (xmlNode_0.Value != null)
				{
					int num = Class34.smethod_4(xmlNode_0, class3_0, xmlNode_1);
					if (class3_0.int_0 == -1)
					{
						class3_0.int_0 = num - 1;
					}
					if (num > class3_0.int_0)
					{
						XmlNode xmlNode = xmlNode_0.ParentNode.ParentNode.ParentNode.SelectSingleNode("*[local-name()='content']");
						if (xmlNode != null)
						{
							class3_0.list_0.Add(new Class3.Struct0(xmlNode_0.Value, xmlNode.Attributes["src"].Value.ToString(), num - class3_0.int_0));
							return;
						}
						class3_0.list_0.Add(new Class3.Struct0(xmlNode_0.Value, "", num - class3_0.int_0));
						return;
					}
				}
			}
			else
			{
				foreach (object obj in xmlNode_0.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					Class34.smethod_89(xmlNode2, class3_0, xmlNode_1);
				}
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000345F0 File Offset: 0x000327F0
		static void smethod_90(Form1 form1_0)
		{
			string text = Path.GetExtension(form1_0.struct1_0.string_4).ToLower();
			try
			{
				if (!(text == ".mobi") && !(text == ".azw3"))
				{
					File.Copy(form1_0.string_1 + "\\" + form1_0.struct1_0.string_2 + text, form1_0.comboBox_27.Text, true);
				}
				else
				{
					File.Copy(form1_0.string_1 + "\\OEBPS\\" + form1_0.struct1_0.string_2 + ".mobi", form1_0.comboBox_27.Text, true);
				}
				form1_0.stopwatch_0.Stop();
				Class34.smethod_118(form1_0, string.Format("成功创建 {0}, 用时{1}毫秒", form1_0.comboBox_27.Text, form1_0.stopwatch_0.ElapsedMilliseconds), "normal");
				if (!form1_0.checkBox_6.Checked)
				{
					DialogResult dialogResult = Class34.smethod_27(form1_0, "成功创建文件\n点击\"确定\"在文件管理器中定位文件\n点击\"取消\"返回程序", "EasyPub", MessageBoxButtons.OKCancel);
					if (dialogResult == DialogResult.OK)
					{
						try
						{
							Process.Start("explorer.exe", "/select," + form1_0.comboBox_27.Text);
						}
						catch (Exception ex)
						{
							Class34.smethod_118(form1_0, ex.Message, "error");
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Class34.smethod_118(form1_0, ex2.Message, "error");
				Class34.smethod_153(form1_0, "创建文件失败！", "EasyPub");
			}
			File.Delete(form1_0.string_1 + "\\" + form1_0.struct1_0.string_2 + text);
			Class34.smethod_158(form1_0.string_1 + "\\OEBPS");
			Class34.smethod_158(form1_0.string_1 + "\\META-INF");
			try
			{
				if (Directory.Exists(form1_0.string_1 + "\\OEBPS"))
				{
					Directory.Delete(form1_0.string_1 + "\\OEBPS", false);
				}
				if (Directory.Exists(form1_0.string_1 + "\\META-INF"))
				{
					Directory.Delete(form1_0.string_1 + "\\META-INF", false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00034840 File Offset: 0x00032A40
		static byte[] smethod_91(int int_0, Class13 class13_0, byte[] byte_0, byte[] byte_1)
		{
			byte[] array = new byte[int_0 + byte_0.Length];
			Buffer.BlockCopy(byte_1, 0, array, 0, int_0);
			Buffer.BlockCopy(byte_0, 0, array, int_0, byte_0.Length);
			return array;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00008729 File Offset: 0x00006929
		static void smethod_92(EventArgs0 eventArgs0_0, Class18 class18_0)
		{
			if (class18_0.delegate1_0 != null)
			{
				class18_0.delegate1_0(class18_0, eventArgs0_0);
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00034870 File Offset: 0x00032A70
		static int smethod_93(Class27.Class29 class29_0, byte[] byte_0, int int_0, int int_1)
		{
			int num = 0;
			while (class29_0.int_2 > 0 && int_1 > 0)
			{
				byte_0[int_0++] = (byte)class29_0.uint_0;
				class29_0.uint_0 >>= 8;
				class29_0.int_2 -= 8;
				int_1--;
				num++;
			}
			if (int_1 == 0)
			{
				return num;
			}
			int num2 = class29_0.int_1 - class29_0.int_0;
			if (int_1 > num2)
			{
				int_1 = num2;
			}
			Array.Copy(class29_0.byte_0, class29_0.int_0, byte_0, int_0, int_1);
			class29_0.int_0 += int_1;
			if (((class29_0.int_0 - class29_0.int_1) & 1) != 0)
			{
				class29_0.uint_0 = (uint)(class29_0.byte_0[class29_0.int_0++] & byte.MaxValue);
				class29_0.int_2 = 8;
			}
			return num + int_1;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00034940 File Offset: 0x00032B40
		static int smethod_94(int int_0, Class18 class18_0, out ListViewItem listViewItem_0, int int_1)
		{
			listViewItem_0 = class18_0.GetItemAt(int_1, int_0);
			if (listViewItem_0 != null)
			{
				int[] array = class18_0.method_0();
				int num = listViewItem_0.GetBounds(ItemBoundsPortion.Entire).Left;
				for (int i = 0; i < array.Length; i++)
				{
					ColumnHeader columnHeader = class18_0.Columns[array[i]];
					if (int_1 < num + columnHeader.Width)
					{
						return columnHeader.Index;
					}
					num += columnHeader.Width;
				}
			}
			return -1;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000349B4 File Offset: 0x00032BB4
		static void smethod_95(Delegate1 delegate1_0, Class18 class18_0)
		{
			Delegate1 @delegate = class18_0.delegate1_0;
			Delegate1 delegate2;
			do
			{
				delegate2 = @delegate;
				Delegate1 delegate3 = (Delegate1)Delegate.Combine(delegate2, delegate1_0);
				@delegate = Interlocked.CompareExchange<Delegate1>(ref class18_0.delegate1_0, delegate3, delegate2);
			}
			while (@delegate != delegate2);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000349EC File Offset: 0x00032BEC
		static byte[] smethod_96(byte[] byte_0, int int_0, int int_1, byte[] byte_1, Class13 class13_0)
		{
			byte[] array = new byte[byte_1.Length + int_0];
			Buffer.BlockCopy(byte_1, 0, array, 0, byte_1.Length);
			Buffer.BlockCopy(byte_0, int_1, array, byte_1.Length, int_0);
			return array;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00034A20 File Offset: 0x00032C20
		static bool smethod_97(Class27.Class28 class28_0)
		{
			switch (class28_0.int_4)
			{
			case 2:
			{
				if (class28_0.bool_0)
				{
					class28_0.int_4 = 12;
					return false;
				}
				int num = Class34.smethod_21(class28_0.class29_0, 3);
				if (num < 0)
				{
					return false;
				}
				Class34.smethod_8(class28_0.class29_0, 3);
				if ((num & 1) != 0)
				{
					class28_0.bool_0 = true;
				}
				switch (num >> 1)
				{
				case 0:
					Class34.smethod_47(class28_0.class29_0);
					class28_0.int_4 = 3;
					break;
				case 1:
					class28_0.class31_0 = Class27.Class31.class31_0;
					class28_0.class31_1 = Class27.Class31.class31_1;
					class28_0.int_4 = 7;
					break;
				case 2:
					class28_0.class32_0 = new Class27.Class32();
					class28_0.int_4 = 6;
					break;
				}
				return true;
			}
			case 3:
				if ((class28_0.int_8 = Class34.smethod_21(class28_0.class29_0, 16)) < 0)
				{
					return false;
				}
				Class34.smethod_8(class28_0.class29_0, 16);
				class28_0.int_4 = 4;
				break;
			case 4:
				break;
			case 5:
				goto IL_0135;
			case 6:
				if (!Class34.smethod_129(class28_0.class32_0, class28_0.class29_0))
				{
					return false;
				}
				class28_0.class31_0 = Class34.smethod_63(class28_0.class32_0);
				class28_0.class31_1 = Class34.smethod_175(class28_0.class32_0);
				class28_0.int_4 = 7;
				goto IL_01BB;
			case 7:
			case 8:
			case 9:
			case 10:
				goto IL_01BB;
			case 11:
				return false;
			case 12:
				return false;
			default:
				return false;
			}
			int num2 = Class34.smethod_21(class28_0.class29_0, 16);
			if (num2 < 0)
			{
				return false;
			}
			Class34.smethod_8(class28_0.class29_0, 16);
			class28_0.int_4 = 5;
			IL_0135:
			int num3 = Class34.smethod_19(class28_0.class30_0, class28_0.class29_0, class28_0.int_8);
			class28_0.int_8 -= num3;
			if (class28_0.int_8 == 0)
			{
				class28_0.int_4 = 2;
				return true;
			}
			return !Class34.smethod_136(class28_0.class29_0);
			IL_01BB:
			return Class34.smethod_146(class28_0);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00034BF4 File Offset: 0x00032DF4
		static bool smethod_98(Form2 form2_0)
		{
			bool flag = true;
			int i = 0;
			while (i < form2_0.class18_0.Items.Count)
			{
				if (Class34.smethod_42(form2_0, form2_0.class18_0.Items[i]))
				{
					i++;
				}
				else
				{
					flag = false;
					IL_003A:
					if (flag)
					{
						Class34.smethod_153(form2_0, "全部目录均已删除，请至少保留一个目录项！", "EasyPub");
						return false;
					}
					Regex regex = new Regex("＋");
					form2_0.list_1.Clear();
					for (int j = 0; j < form2_0.class18_0.Items.Count; j++)
					{
						string text = form2_0.class18_0.Items[j].SubItems[0].Text;
						int num = regex.Matches(text).Count;
						num++;
						if (Class34.smethod_42(form2_0, form2_0.class18_0.Items[j]))
						{
							num = -num;
						}
						string text2 = form2_0.class18_0.Items[j].SubItems[1].Text;
						Class1 @class = new Class1(form2_0.list_0[j].method_0(), text2, form2_0.list_0[j].method_4(), num);
						form2_0.list_1.Add(@class);
					}
					form2_0.list_1.Add(form2_0.list_0[form2_0.list_0.Count - 1]);
					return true;
				}
			}
			goto IL_003A;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00008740 File Offset: 0x00006940
		static void smethod_99(EventArgs0 eventArgs0_0, Class18 class18_0)
		{
			if (class18_0.delegate1_1 != null)
			{
				class18_0.delegate1_1(class18_0, eventArgs0_0);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00005233 File Offset: 0x00003433
		static void smethod_100(SettingChangingEventArgs settingChangingEventArgs_0, object object_0, Settings settings_0)
		{
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00034D60 File Offset: 0x00032F60
		static void smethod_101(Form1 form1_0)
		{
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			form1_0.openFileDialog_0.CheckFileExists = true;
			form1_0.openFileDialog_0.CheckPathExists = true;
			form1_0.openFileDialog_0.Title = "Select Input File...";
			form1_0.openFileDialog_0.Filter = "Text Documents|*.txt|EPUB Documents|*.epub";
			form1_0.saveFileDialog_0.CheckFileExists = false;
			form1_0.saveFileDialog_0.CheckPathExists = false;
			form1_0.saveFileDialog_0.Title = "Save EBook File...";
			form1_0.saveFileDialog_0.Filter = "EBook Documents|*.epub;*.mobi;*.azw3";
			form1_0.openFileDialog_1.CheckFileExists = true;
			form1_0.openFileDialog_1.CheckPathExists = true;
			form1_0.openFileDialog_1.Title = "Select Cover Image...";
			form1_0.openFileDialog_1.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
			form1_0.openFileDialog_2.CheckFileExists = true;
			form1_0.openFileDialog_2.CheckPathExists = true;
			form1_0.openFileDialog_2.Title = "Select Text Editor...";
			form1_0.openFileDialog_2.Filter = "Executable Files|*.exe";
			form1_0.openFileDialog_3.CheckFileExists = true;
			form1_0.openFileDialog_3.CheckPathExists = true;
			if (Directory.Exists(directoryName + "\\fonts"))
			{
				form1_0.openFileDialog_3.InitialDirectory = directoryName + "\\fonts";
			}
			form1_0.openFileDialog_3.Title = "Select Truetype Font...";
			form1_0.openFileDialog_3.Filter = "Truetype Font Files|*.ttf";
			form1_0.openFileDialog_4.CheckFileExists = true;
			form1_0.openFileDialog_4.CheckPathExists = true;
			if (Directory.Exists(directoryName + "\\css"))
			{
				form1_0.openFileDialog_4.InitialDirectory = directoryName + "\\css";
			}
			form1_0.openFileDialog_4.Title = "Select CSS File...";
			form1_0.openFileDialog_4.Filter = "CSS Files|*.css";
			form1_0.saveFileDialog_1.CheckFileExists = true;
			form1_0.saveFileDialog_1.CheckPathExists = true;
			form1_0.saveFileDialog_1.InitialDirectory = directoryName;
			form1_0.saveFileDialog_1.Title = "Select File...";
			form1_0.saveFileDialog_1.Filter = "EasyPub Chapter Info FIles|*.epsav";
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00008757 File Offset: 0x00006957
		static void smethod_102(Form1 form1_0, ComboBox comboBox_0, string string_0)
		{
			if (string_0 == "")
			{
				return;
			}
			if (!comboBox_0.Items.Contains(string_0))
			{
				comboBox_0.Items.Add(string_0);
			}
			comboBox_0.Text = string_0;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00034F5C File Offset: 0x0003315C
		static string smethod_103(Form1 form1_0)
		{
			string[] array = new string[] { "[0123456789一二三四五六七八九十零〇百千两]+", "[一二三四五六七八九十零〇百千两]+", "[0123456789]+" };
			string text;
			if (form1_0.radioButton_4.Checked)
			{
				if (form1_0.checkBox_1.Checked)
				{
					text = "^\\s*";
				}
				else
				{
					text = "^";
				}
				text = string.Concat(new string[]
				{
					text,
					form1_0.comboBox_2.Text,
					"\\s*",
					array[form1_0.comboBox_4.SelectedIndex],
					"\\s*",
					form1_0.comboBox_3.Text
				});
				if (form1_0.comboBox_13.Text != "")
				{
					text = text + "|" + form1_0.comboBox_13.Text;
				}
			}
			else if (form1_0.radioButton_5.Checked)
			{
				text = form1_0.comboBox_12.Text;
			}
			else
			{
				text = "_EASYPUB_REGEX_STRING_NULL_";
			}
			if (text == "")
			{
				text = "_EASYPUB_REGEX_STRING_NULL_";
			}
			return text;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00008789 File Offset: 0x00006989
		static bool smethod_104(string string_0)
		{
			return string_0.Length >= 3 && !(string_0.Substring(1, 2) != ":\\");
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00035070 File Offset: 0x00033270
		static byte[] smethod_105(byte[] byte_0, int int_0, byte[] byte_1)
		{
			Tuple<uint, uint, uint> tuple = Class34.smethod_52(byte_0);
			int item = (int)tuple.Item1;
			int item2 = (int)tuple.Item2;
			int item3 = (int)tuple.Item3;
			int num = item + 12;
			int i = item3;
			while (i != 0)
			{
				int num2 = (int)Class34.smethod_165(byte_0, num, "L");
				if ((ulong)num2 == (ulong)((long)int_0))
				{
					int num3 = byte_1.Length + 8 - (int)Class34.smethod_165(byte_0, num + 4, "L");
					byte[] array = byte_0;
					if (num3 != 0)
					{
						int int_ = Class5.Class6.int_11;
						int num4 = (int)(Class34.smethod_165(array, Class5.Class6.int_11, "L") + (uint)num3);
						string text = "L";
						array = Class34.smethod_110((uint)num4, int_, text, array);
					}
					byte[] array2 = array.smethod_6(0, item + 4);
					int num5 = (int)((uint)((ulong)item2 + (ulong)((long)byte_1.Length) + 8UL - (ulong)Class34.smethod_165(byte_0, num + 4, "L")));
					array2 = array2.smethod_0(((uint)num5).smethod_5());
					array2 = array2.smethod_0(((uint)item3).smethod_5());
					array2 = array2.smethod_0(byte_0.smethod_6(item + 12, num + 4));
					num5 = byte_1.Length + 8;
					array2 = array2.smethod_0(((uint)num5).smethod_5());
					array2 = array2.smethod_0(byte_1);
					return array2.smethod_0(byte_0.smethod_6(num + (int)Class34.smethod_165(byte_0, num + 4, "L"), byte_0.Length));
				}
				i--;
				num += (int)Class34.smethod_165(byte_0, num + 4, "L");
			}
			return byte_0;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000351E0 File Offset: 0x000333E0
		static byte[] smethod_106(int int_0, int int_1, byte[] byte_0, byte[] byte_1)
		{
			byte[] array = new byte[byte_1.Length + int_1];
			Buffer.BlockCopy(byte_1, 0, array, 0, byte_1.Length);
			Buffer.BlockCopy(byte_0, int_0, array, byte_1.Length, int_1);
			return array;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00035214 File Offset: 0x00033414
		static void smethod_107(Form1 form1_0)
		{
			string text = Path.GetDirectoryName(Application.ExecutablePath) + string.Format("\\css\\_easypub_autosave_{0}.css", form1_0.string_4);
			if (File.Exists(text))
			{
				form1_0.textBox_4.Text = File.ReadAllText(text);
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0003525C File Offset: 0x0003345C
		static string smethod_108(string string_0, XmlDocument xmlDocument_0, string string_1)
		{
			try
			{
				string text = xmlDocument_0.SelectSingleNode(string_0).InnerText;
				if (text == "")
				{
					text = string_1;
				}
				return text;
			}
			catch (Exception)
			{
			}
			return string_1;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000087AB File Offset: 0x000069AB
		static int smethod_109(Class27.Class30 class30_0)
		{
			return class30_0.int_1;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000087B3 File Offset: 0x000069B3
		static byte[] smethod_110(uint uint_0, int int_0, string string_0, byte[] byte_0 = "L")
		{
			if (string_0 == "L")
			{
				return byte_0.smethod_1(int_0, uint_0.smethod_5());
			}
			return byte_0.smethod_1(int_0, ((ushort)uint_0).smethod_4());
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000352A0 File Offset: 0x000334A0
		static void smethod_111(Class27.Class30 class30_0, int int_0)
		{
			if (class30_0.int_1++ == 32768)
			{
				throw new InvalidOperationException();
			}
			class30_0.byte_0[class30_0.int_0++] = (byte)int_0;
			class30_0.int_0 &= 32767;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000352F8 File Offset: 0x000334F8
		static ResourceManager smethod_112()
		{
			if (object.ReferenceEquals(Class23.resourceManager_0, null))
			{
				ResourceManager resourceManager = new ResourceManager("ns12.Class23", typeof(Class23).Assembly);
				Class23.resourceManager_0 = resourceManager;
			}
			return Class23.resourceManager_0;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00035338 File Offset: 0x00033538
		static string smethod_113(string string_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MD5 md = MD5.Create();
			using (FileStream fileStream = File.OpenRead(string_0))
			{
				byte[] array = md.ComputeHash(fileStream);
				for (int i = 0; i < array.Length; i++)
				{
					bool flag = array[i] != 0;
					stringBuilder.Append(flag.ToString("x2").ToLower());
				}
			}
			string text = stringBuilder.ToString();
			md.Clear();
			return text;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000353BC File Offset: 0x000335BC
		static void smethod_114(Form1 form1_0)
		{
			if (!Directory.Exists(Path.GetDirectoryName(form1_0.struct1_0.string_4)))
			{
				DialogResult dialogResult = Class34.smethod_27(form1_0, string.Format("输出文件路径不存在!\n{0}\n点击”确定“创建此目录并继续\n点击”取消“返回并重新选择输出目录", Path.GetDirectoryName(form1_0.struct1_0.string_4)), "EasyPub", MessageBoxButtons.OKCancel);
				if (dialogResult != DialogResult.OK)
				{
					return;
				}
				Directory.CreateDirectory(Path.GetDirectoryName(form1_0.struct1_0.string_4));
			}
			try
			{
				Class34.smethod_118(form1_0, "正在创建mobi文件", "normal");
				int num = 0;
				if (form1_0.radioButton_9.Checked)
				{
					num = 0;
				}
				else if (form1_0.radioButton_8.Checked)
				{
					num = 1;
				}
				else if (form1_0.radioButton_7.Checked)
				{
					num = 2;
				}
				Class3 @class = new Class3(form1_0.textBox_4.Text, num);
				string text = form1_0.textBox_0.Text;
				string text2 = Path.GetDirectoryName(form1_0.struct1_0.string_4) + "\\" + form1_0.struct1_0.string_2 + ".epub";
				if (!Class34.smethod_120(form1_0.checkBox_13.Checked, text, @class, text2))
				{
					return;
				}
				form1_0.string_5 = Path.GetDirectoryName(Application.ExecutablePath) + "\\bin\\" + form1_0.comboBox_16.Text;
				form1_0.string_6 = string.Format(" {0} -{1} {2} -o {3}_out.mobi", new object[]
				{
					form1_0.struct1_0.string_2 + ".epub",
					form1_0.comboBox_17.Text,
					form1_0.textBox_13.Text.Trim(),
					form1_0.struct1_0.string_2
				});
				form1_0.string_7 = Path.GetDirectoryName(form1_0.struct1_0.string_4);
				Thread thread = new Thread(new ThreadStart(form1_0.method_57));
				Form1.bool_2 = true;
				thread.Start();
				form1_0.form0_0 = new Form0("正在创建mobi文件...");
				form1_0.form0_0.ShowInTaskbar = false;
				form1_0.form0_0.ShowDialog(form1_0);
				while (thread.IsAlive && Form1.bool_2)
				{
					Application.DoEvents();
				}
				File.Delete(Path.GetDirectoryName(form1_0.struct1_0.string_4) + "\\" + form1_0.struct1_0.string_2 + ".epub");
				try
				{
					if (form1_0.checkBox_11.Checked)
					{
						string text3 = form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + "_out.mobi";
						string text4 = form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi";
						if (!Class34.smethod_10(text4, text3))
						{
							File.Copy(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + "_out.mobi", form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi", true);
						}
						File.Delete(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + "_out.mobi");
					}
					else
					{
						File.Copy(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + "_out.mobi", form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi", true);
						File.Delete(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + "_out.mobi");
					}
				}
				catch (Exception ex)
				{
					Class34.smethod_118(form1_0, ex.Message, "error");
					return;
				}
				Dictionary<int, string> dictionary = new Dictionary<int, string>();
				dictionary.Add(300, "_EASYPUB_REMOVE_");
				string text5 = "EBOK";
				if (form1_0.checkBox_10.Checked)
				{
					string text6 = form1_0.textBox_14.Text.Trim().ToUpper();
					string text7;
					if (form1_0.radioButton_10.Checked && Regex.IsMatch(text6, "^B00[A-Z0-9]{7}|^\\d{10}"))
					{
						text7 = text6;
					}
					else
					{
						text7 = "B00" + Class34.smethod_74(7, form1_0);
					}
					dictionary.Add(113, text7);
					dictionary.Add(501, text5);
				}
				try
				{
					Class13 class2 = new Class13();
					if (!Class34.smethod_36(dictionary, form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi", class2))
					{
						Class34.smethod_118(form1_0, "MOBI文件格式错误！", "error");
						return;
					}
				}
				catch (Exception ex2)
				{
					Class34.smethod_118(form1_0, ex2.Message, "error");
					return;
				}
			}
			catch (Exception ex3)
			{
				Class34.smethod_118(form1_0, ex3.Message, "error");
				return;
			}
			if (form1_0.comboBox_28.SelectedIndex == 1)
			{
				byte[] array = Class5.smethod_0(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi", "mobi7");
				File.WriteAllBytes(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi", array);
			}
			if (form1_0.comboBox_28.SelectedIndex == 2)
			{
				byte[] array2 = Class5.smethod_0(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi", "mobi8");
				File.WriteAllBytes(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi", array2);
			}
			File.Copy(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi", form1_0.comboBox_27.Text, true);
			File.Delete(form1_0.string_7 + "\\" + form1_0.struct1_0.string_2 + ".mobi");
			form1_0.stopwatch_0.Stop();
			if (!form1_0.checkBox_14.Checked)
			{
				form1_0.struct3_0.string_22 = Path.GetDirectoryName(form1_0.comboBox_27.Text);
			}
			Class34.smethod_118(form1_0, string.Format("成功创建 {0}，用时{1}毫秒", form1_0.comboBox_27.Text, form1_0.stopwatch_0.ElapsedMilliseconds), "normal");
			if (!form1_0.checkBox_6.Checked)
			{
				DialogResult dialogResult2 = Class34.smethod_27(form1_0, "成功创建文件\n点击\"确定\"在文件管理器中定位文件\n点击\"取消\"返回程序", "EasyPub", MessageBoxButtons.OKCancel);
				if (dialogResult2 == DialogResult.OK)
				{
					try
					{
						Process.Start("explorer.exe", "/select," + form1_0.comboBox_27.Text);
					}
					catch (Exception ex4)
					{
						Class34.smethod_118(form1_0, ex4.Message, "error");
					}
				}
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00035A98 File Offset: 0x00033C98
		static void smethod_115(Form2 form2_0, ListViewItem listViewItem_0, bool bool_0)
		{
			if (bool_0)
			{
				listViewItem_0.BackColor = form2_0.color_1;
				listViewItem_0.Font = new Font(listViewItem_0.Font, listViewItem_0.Font.Style | FontStyle.Strikeout);
				return;
			}
			listViewItem_0.Font = new Font(listViewItem_0.Font, listViewItem_0.Font.Style & ~FontStyle.Strikeout);
			if (form2_0.class18_0.Items.IndexOf(listViewItem_0) % 2 == 1)
			{
				listViewItem_0.BackColor = form2_0.color_0;
				return;
			}
			listViewItem_0.BackColor = global::System.Drawing.Color.White;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00035B20 File Offset: 0x00033D20
		static byte[] smethod_116(byte[] byte_0, int int_0, byte[] byte_1)
		{
			byte[] array = new byte[int_0 + byte_1.Length];
			Buffer.BlockCopy(byte_0, 0, array, 0, int_0);
			Buffer.BlockCopy(byte_1, 0, array, int_0, byte_1.Length);
			return array;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00035B50 File Offset: 0x00033D50
		static void smethod_117(string string_0, Form1 form1_0)
		{
			if (!File.Exists(string_0))
			{
				Class34.smethod_153(form1_0, string.Format("文件{0}不存在，请重新检查！", string_0), "EasyPub");
				return;
			}
			if (File.GetLastWriteTimeUtc(Path.GetFullPath(string_0)) != form1_0.dateTime_0)
			{
				Encoding encoding = Class34.smethod_179(string_0);
				StreamReader streamReader = new StreamReader(string_0, encoding);
				if (streamReader.Peek() > -1)
				{
					Form1.list_0.Clear();
					while (streamReader.Peek() > 0)
					{
						string text = streamReader.ReadLine();
						Form1.list_0.Add(text);
					}
					form1_0.dateTime_0 = File.GetLastWriteTimeUtc(string_0);
				}
				streamReader.Close();
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00035BE8 File Offset: 0x00033DE8
		static void smethod_118(Form1 form1_0, string string_0, string string_1)
		{
			string text = string_1.ToLower();
			if (text == "normal")
			{
				form1_0.textBox_12.Text = string_0;
				form1_0.textBox_12.BackColor = SystemColors.Control;
				form1_0.textBox_12.ForeColor = global::System.Drawing.Color.Black;
			}
			else if (text == "warning")
			{
				form1_0.textBox_12.Text = string_0;
				form1_0.textBox_12.BackColor = SystemColors.Info;
				form1_0.textBox_12.ForeColor = global::System.Drawing.Color.Black;
			}
			else if (text == "error")
			{
				form1_0.textBox_12.Text = string_0;
				form1_0.textBox_12.BackColor = form1_0.color_0;
				form1_0.textBox_12.ForeColor = global::System.Drawing.Color.Black;
			}
			else
			{
				form1_0.textBox_12.Text = string_0;
			}
			form1_0.textBox_12.Refresh();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000087DE File Offset: 0x000069DE
		static int smethod_119(Class27.Class29 class29_0)
		{
			return class29_0.int_2;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00035CC8 File Offset: 0x00033EC8
		static bool smethod_120(bool bool_0, string string_0, Class3 class3_0, string string_1)
		{
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				using (ZipFile zipFile = ZipFile.Read(string_0))
				{
					ZipEntry zipEntry = zipFile["META-INF/container.xml"];
					MemoryStream memoryStream = new MemoryStream();
					zipEntry.Extract(memoryStream);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					xmlDocument.XmlResolver = null;
					xmlDocument.Load(memoryStream);
					string text = "";
					string text2 = "";
					string text3 = "";
					string text4 = "";
					XmlNode xmlNode = xmlDocument.SelectSingleNode("/*[local-name()='container']/*[local-name()='rootfiles']/*[local-name()='rootfile']");
					if (xmlNode != null)
					{
						text = xmlNode.Attributes["full-path"].Value;
					}
					if (text == "" || zipFile[text] == null)
					{
						Class34.smethod_153(Form.ActiveForm, "OPF文件不存在！", "EasyPub");
						return false;
					}
					memoryStream.SetLength(0L);
					if (text.LastIndexOf("/") != -1)
					{
						text4 = text.Substring(0, text.LastIndexOf("/") + 1);
					}
					zipFile[text].Extract(memoryStream);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					xmlDocument.XmlResolver = null;
					xmlDocument.Load(memoryStream);
					XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/*[translate(local-name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(local-name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='metadata']/*[translate(local-name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='title']");
					if (xmlNode2 == null)
					{
						Class34.smethod_153(Form.ActiveForm, "epub标题不能为空！", "EasyPub");
						return false;
					}
					xmlNode = xmlDocument.SelectSingleNode("/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='manifest']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='item' and @media-type='application/x-dtbncx+xml' ]");
					if (xmlNode != null)
					{
						text3 = text4 + xmlNode.Attributes["href"].Value;
					}
					if (text3 == "" || zipFile[text3] == null)
					{
						Class34.smethod_153(Form.ActiveForm, "NCX文件不存在！", "EasyPub");
						return false;
					}
					xmlNode = xmlDocument.SelectSingleNode("/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='manifest']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='item' and @media-type='text/css' ]");
					if (xmlNode != null)
					{
						text2 = text4 + xmlNode.Attributes["href"].Value;
					}
					if (text2 != "")
					{
						string text5 = Class34.smethod_28(text2, class3_0, zipFile);
						zipFile.UpdateEntry(text2, text5, Encoding.UTF8);
					}
					string text6 = "";
					xmlNode = xmlDocument.SelectSingleNode("/*[translate(local-name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(local-name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='metadata']/*[translate(local-name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='language']");
					if (xmlNode == null)
					{
						text6 = "zh-CN";
						if (bool_0)
						{
							text6 = "en-US";
						}
						XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/*[translate(local-name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(local-name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='metadata']");
						XmlNode xmlNode4 = xmlDocument.CreateElement(xmlNode2.Name.ToLower().Replace("title", "language"), xmlDocument.DocumentElement.NamespaceURI);
						xmlNode4.InnerText = text6;
						xmlNode3.AppendChild(xmlNode4);
					}
					else
					{
						text6 = xmlNode.InnerText;
						if (bool_0)
						{
							text6 = "en-US";
						}
						if (text6 != "zh")
						{
							try
							{
								CultureInfo.GetCultureInfo(text6);
							}
							catch (Exception)
							{
								text6 = "zh-CN";
							}
						}
						if (text6 != xmlNode.InnerText)
						{
							xmlNode.InnerText = text6;
						}
					}
					if (xmlDocument.SelectSingleNode("/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='guide']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='reference' and @type='toc' ]") == null)
					{
						string text7 = class3_0.method_0(text3, text2, zipFile);
						if (text7.Length > 200)
						{
							zipFile.AddEntry(text4 + "easypub-toc.html", text7, Encoding.UTF8);
							text7 = "easypub-toc.html";
							XmlNode xmlNode5 = xmlDocument.SelectSingleNode("/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='manifest']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='item']");
							XmlNode xmlNode6 = xmlNode5.Clone();
							xmlNode6.Attributes["id"].Value = "easypubautotoc";
							xmlNode6.Attributes["href"].Value = text7;
							xmlNode6.Attributes["media-type"].Value = "application/xhtml+xml";
							XmlNode xmlNode7 = xmlDocument.SelectSingleNode("/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='manifest']");
							xmlNode7.AppendChild(xmlNode6);
						}
						XmlNode xmlNode8 = xmlDocument.SelectSingleNode("/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='guide']");
						if (xmlNode8 != null)
						{
							XmlNode xmlNode9 = xmlDocument.SelectSingleNode("/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='guide']/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='reference']");
							if (xmlNode9 != null)
							{
								XmlElement xmlElement = xmlDocument.CreateElement("reference", xmlDocument.DocumentElement.NamespaceURI);
								xmlElement.SetAttribute("href", xmlDocument.DocumentElement.NamespaceURI, text7);
								xmlElement.SetAttribute("title", xmlDocument.DocumentElement.NamespaceURI, "Table Of Contents");
								xmlElement.SetAttribute("type", xmlDocument.DocumentElement.NamespaceURI, "toc");
								xmlNode8.AppendChild(xmlElement);
							}
						}
						else
						{
							XmlNode xmlNode10 = xmlDocument.CreateNode(XmlNodeType.Element, "guide", xmlDocument.DocumentElement.NamespaceURI);
							xmlNode10.InnerXml = string.Format("<reference href=\"{0}\" title=\"Table Of Contents\" type=\"toc\" />", text7);
							xmlDocument.SelectSingleNode("/*[translate(name(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='package']").AppendChild(xmlNode10);
						}
					}
					memoryStream.Seek(0L, SeekOrigin.Begin);
					xmlDocument.Save(memoryStream);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					zipFile.UpdateEntry(text, memoryStream);
					zipFile.Save(string_1);
				}
			}
			catch (Exception ex)
			{
				Class34.smethod_169("epub格式错误\n" + ex.Message, "EasyPub");
				return false;
			}
			return true;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000087E6 File Offset: 0x000069E6
		static bool smethod_121(Class10 class10_0)
		{
			return class10_0.int_0 >= class10_0.string_0.Length;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00036220 File Offset: 0x00034420
		static bool smethod_122(Form1 form1_0)
		{
			if (form1_0.bool_3)
			{
				return false;
			}
			form1_0.string_3 = form1_0.textBox_0.Text;
			if (!File.Exists(form1_0.string_3))
			{
				Class34.smethod_153(form1_0, "请先选择文件！", "EasyPub");
				return false;
			}
			string text = Class34.smethod_113(form1_0.string_3);
			string text2 = text + Class34.smethod_103(form1_0);
			if (form1_0.string_2 != text2)
			{
				form1_0.string_2 = text2;
				return true;
			}
			return false;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000087FE File Offset: 0x000069FE
		static void smethod_123(Class10.Class11 class11_0, bool bool_0)
		{
			if (bool_0)
			{
				if (class11_0.stringBuilder_1.Length > 0)
				{
					Class34.smethod_0(class11_0);
				}
				class11_0.int_0 = 0;
			}
			class11_0.bool_0 = bool_0;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0003629C File Offset: 0x0003449C
		static Class21 smethod_124(Form form_0)
		{
			Class21 @class = new Class21();
			@class.intptr_0 = Class34.GetSystemMenu_1(form_0.Handle, 0);
			if (@class.intptr_0 == IntPtr.Zero)
			{
				throw new Exception0();
			}
			return @class;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000362DC File Offset: 0x000344DC
		static void smethod_125(string string_0)
		{
			try
			{
				byte[] array = File.ReadAllBytes(string_0);
				byte[] array2 = Class15.smethod_0(array);
				File.WriteAllBytes(string_0, array2);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00036314 File Offset: 0x00034514
		static List<byte[]> smethod_126(byte[] byte_0, int int_0)
		{
			List<byte[]> list = new List<byte[]>();
			Tuple<uint, uint, uint> tuple = Class34.smethod_52(byte_0);
			int num = (int)tuple.Item1;
			uint item = tuple.Item2;
			int i = (int)tuple.Item3;
			num += 12;
			while (i != 0)
			{
				int num2 = (int)Class34.smethod_165(byte_0, num, "L");
				if ((ulong)num2 == (ulong)((long)int_0))
				{
					list.Add(byte_0.smethod_6(num + 8, num + (int)Class34.smethod_165(byte_0, num + 4, "L")));
				}
				i--;
				num += (int)Class34.smethod_165(byte_0, num + 4, "L");
			}
			return list;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00036398 File Offset: 0x00034598
		static void smethod_127(string string_0, Form1 form1_0)
		{
			if (!Class34.smethod_122(form1_0))
			{
				return;
			}
			bool flag = false;
			form1_0.string_3 = form1_0.textBox_0.Text;
			if (File.Exists(form1_0.string_3))
			{
				Class34.smethod_117(form1_0.string_3, form1_0);
				form1_0.string_2 = Class34.smethod_113(form1_0.string_3) + string_0;
				form1_0.list_1.Clear();
				if (string_0 != "")
				{
					Regex regex = new Regex(string_0);
					for (int i = 0; i < Form1.list_0.Count - 1; i++)
					{
						string text = Form1.list_0[i];
						if (regex.IsMatch(text))
						{
							flag = true;
							form1_0.list_1.Add(new Class1(text, i, 1));
						}
					}
					if (flag)
					{
						form1_0.list_1.Add(new Class1("_EASYPUB_END_OF_INPUT_FILE_", Form1.list_0.Count, 1));
						string text = "";
						if (form1_0.list_1[0].method_4() > 0)
						{
							for (int j = 0; j < form1_0.list_1[0].method_4(); j++)
							{
								text += Form1.list_0[j].Trim();
							}
						}
						if (text != "")
						{
							form1_0.list_1.Insert(0, new Class1("_EASYPUB_AUTO_PREFACE_", "序", 0, 1));
						}
					}
				}
				return;
			}
			Class34.smethod_153(form1_0, "请先选择文件！", "EasyPub");
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0003650C File Offset: 0x0003470C
		static string smethod_128(Class10 class10_0, string string_0)
		{
			class10_0.class11_0 = new Class10.Class11();
			class10_0.string_0 = string_0;
			class10_0.int_0 = 0;
			while (!Class34.smethod_121(class10_0))
			{
				if (Class34.smethod_88(class10_0) == '<')
				{
					bool flag;
					string text = Class34.smethod_72(class10_0, ref flag);
					if (text == "body")
					{
						Class34.smethod_168(class10_0.class11_0);
					}
					else if (text == "/body")
					{
						class10_0.int_0 = class10_0.string_0.Length;
					}
					else if (text == "pre")
					{
						Class34.smethod_123(class10_0.class11_0, true);
						Class34.smethod_31(class10_0);
					}
					else if (text == "/pre")
					{
						Class34.smethod_123(class10_0.class11_0, false);
					}
					string text2;
					if (Class10.dictionary_0.TryGetValue(text, out text2))
					{
						Class34.smethod_84(text2, class10_0.class11_0);
					}
					if (Class10.list_0.Contains(text))
					{
						Class34.smethod_152(text, class10_0);
					}
				}
				else if (Class34.smethod_145(Class34.smethod_88(class10_0)))
				{
					Class34.smethod_7(class10_0.class11_0, class10_0.class11_0.bool_0 ? Class34.smethod_88(class10_0) : ' ');
					Class34.smethod_87(class10_0);
				}
				else
				{
					Class34.smethod_7(class10_0.class11_0, Class34.smethod_88(class10_0));
					Class34.smethod_87(class10_0);
				}
			}
			return HttpUtility.HtmlDecode(class10_0.class11_0.ToString());
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0003665C File Offset: 0x0003485C
		static bool smethod_129(Class27.Class32 class32_0, Class27.Class29 class29_0)
		{
			for (;;)
			{
				switch (class32_0.int_2)
				{
				case 0:
					class32_0.int_3 = Class34.smethod_21(class29_0, 5);
					if (class32_0.int_3 >= 0)
					{
						class32_0.int_3 += 257;
						Class34.smethod_8(class29_0, 5);
						class32_0.int_2 = 1;
						goto IL_01BE;
					}
					return false;
				case 1:
					goto IL_01BE;
				case 2:
					goto IL_0170;
				case 3:
					goto IL_0137;
				case 4:
					break;
				case 5:
					goto IL_0006;
				default:
					continue;
				}
				IL_00C2:
				int num;
				while (((num = Class34.smethod_12(class32_0.class31_0, class29_0)) & -16) == 0)
				{
					class32_0.byte_1[class32_0.int_8++] = (class32_0.byte_2 = (byte)num);
					if (class32_0.int_8 == class32_0.int_6)
					{
						return true;
					}
				}
				if (num >= 0)
				{
					if (num >= 17)
					{
						class32_0.byte_2 = 0;
					}
					class32_0.int_7 = num - 16;
					class32_0.int_2 = 5;
					goto IL_0006;
				}
				return false;
				IL_0137:
				while (class32_0.int_8 < class32_0.int_5)
				{
					int num2 = Class34.smethod_21(class29_0, 3);
					if (num2 < 0)
					{
						return false;
					}
					Class34.smethod_8(class29_0, 3);
					class32_0.byte_0[Class27.Class32.int_9[class32_0.int_8]] = (byte)num2;
					class32_0.int_8++;
				}
				class32_0.class31_0 = new Class27.Class31(class32_0.byte_0);
				class32_0.byte_0 = null;
				class32_0.int_8 = 0;
				class32_0.int_2 = 4;
				goto IL_00C2;
				IL_0006:
				int num3 = Class27.Class32.int_1[class32_0.int_7];
				int num4 = Class34.smethod_21(class29_0, num3);
				if (num4 < 0)
				{
					return false;
				}
				Class34.smethod_8(class29_0, num3);
				num4 += Class27.Class32.int_0[class32_0.int_7];
				while (num4-- > 0)
				{
					class32_0.byte_1[class32_0.int_8++] = class32_0.byte_2;
				}
				if (class32_0.int_8 == class32_0.int_6)
				{
					break;
				}
				class32_0.int_2 = 4;
				continue;
				IL_0170:
				class32_0.int_5 = Class34.smethod_21(class29_0, 4);
				if (class32_0.int_5 >= 0)
				{
					class32_0.int_5 += 4;
					Class34.smethod_8(class29_0, 4);
					class32_0.byte_0 = new byte[19];
					class32_0.int_8 = 0;
					class32_0.int_2 = 3;
					goto IL_0137;
				}
				return false;
				IL_01BE:
				class32_0.int_4 = Class34.smethod_21(class29_0, 5);
				if (class32_0.int_4 >= 0)
				{
					class32_0.int_4++;
					Class34.smethod_8(class29_0, 5);
					class32_0.int_6 = class32_0.int_3 + class32_0.int_4;
					class32_0.byte_1 = new byte[class32_0.int_6];
					class32_0.int_2 = 2;
					goto IL_0170;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000368F8 File Offset: 0x00034AF8
		static byte[] smethod_130(byte[] byte_0, int int_0)
		{
			Tuple<uint, uint, uint> tuple = Class34.smethod_52(byte_0);
			int item = (int)tuple.Item1;
			int item2 = (int)tuple.Item2;
			int item3 = (int)tuple.Item3;
			int num = item + 12;
			int i = 0;
			while (i < item3)
			{
				int num2 = (int)Class34.smethod_165(byte_0, num, "L");
				int num3 = (int)Class34.smethod_165(byte_0, num + 4, "L");
				if ((ulong)num2 == (ulong)((long)int_0))
				{
					int int_ = Class5.Class6.int_11;
					int num4 = (int)(Class34.smethod_165(byte_0, Class5.Class6.int_11, "L") - (uint)num3);
					string text = "L";
					byte[] array = Class34.smethod_110((uint)num4, int_, text, byte_0);
					byte[] array2 = new byte[0];
					array2 = array.smethod_6(0, num);
					array2 = array2.smethod_0(array.smethod_6(num + num3, array.Length));
					array = array2;
					array2 = array.smethod_6(0, item + 4);
					array2 = array2.smethod_0(((uint)(item2 - num3)).smethod_5());
					array2 = array2.smethod_0(((uint)(item3 - 1)).smethod_5());
					return array2.smethod_0(array.smethod_6(item + 12, array.Length));
				}
				i++;
				num += num3;
			}
			return byte_0;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00036A1C File Offset: 0x00034C1C
		static uint smethod_131(byte[] byte_0)
		{
			int num = 0;
			byte[] array = byte_0.smethod_7(4);
			for (int i = 0; i < array.Length / 4; i++)
			{
				num += (int)array.smethod_3(i * 4);
			}
			return (uint)num;
		}

		// Token: 0x06000678 RID: 1656
		[DllImport("user32.dll")]
		static extern IntPtr CallNextHookEx(IntPtr intptr_0, int int_0, IntPtr intptr_1, IntPtr intptr_2);

		// Token: 0x06000679 RID: 1657 RVA: 0x00008825 File Offset: 0x00006A25
		static void smethod_132(bool bool_0, Class18 class18_0)
		{
			class18_0.bool_1 = bool_0;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00036A50 File Offset: 0x00034C50
		static void smethod_133(Class18 class18_0, Point point_0)
		{
			int x = point_0.X;
			int y = point_0.Y;
			ListViewItem listViewItem;
			int num = Class34.smethod_94(y, class18_0, out listViewItem, x);
			if (num >= 0)
			{
				Class34.smethod_92(new EventArgs0(listViewItem, num), class18_0);
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00036A8C File Offset: 0x00034C8C
		static void smethod_134(Class10 class10_0)
		{
			char c = Class34.smethod_88(class10_0);
			if (c == '"' || c == '\'')
			{
				Class34.smethod_87(class10_0);
				class10_0.int_0 = class10_0.string_0.IndexOfAny(new char[] { c, '\r', '\n' }, class10_0.int_0);
				if (class10_0.int_0 < 0)
				{
					class10_0.int_0 = class10_0.string_0.Length;
					return;
				}
				Class34.smethod_87(class10_0);
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00036B00 File Offset: 0x00034D00
		static byte[] smethod_135(byte[] byte_0, int int_0, byte[] byte_1, int int_1, int int_2)
		{
			byte[] array = new byte[0];
			short num = (short)((ushort)Class34.smethod_165(byte_0, Class5.Class6.int_1, "H"));
			Tuple<int, int> tuple = Class34.smethod_81(byte_0, 0);
			int item = tuple.Item1;
			int item2 = tuple.Item2;
			tuple = Class34.smethod_81(byte_0, int_0);
			int item3 = tuple.Item1;
			int item4 = tuple.Item2;
			int num2 = int_1 - int_2 + 1;
			tuple = Class34.smethod_81(byte_1, int_2);
			int item5 = tuple.Item1;
			int item6 = tuple.Item2;
			tuple = Class34.smethod_81(byte_1, int_1);
			int item7 = tuple.Item1;
			int item8 = tuple.Item2;
			int num3 = item + 8 * num2;
			array = array.smethod_0(byte_0.smethod_6(0, Class5.Class6.int_0));
			int num4 = 2 * ((int)num + num2) + 1;
			array = array.smethod_0(((uint)num4).smethod_5());
			array = array.smethod_0(byte_0.smethod_6(Class5.Class6.int_0 + 4, Class5.Class6.int_1));
			array = array.smethod_0(((ushort)((int)num + num2)).smethod_4());
			for (int i = 0; i < int_0; i++)
			{
				int num5 = (int)byte_0.smethod_3(Class5.Class6.int_4 + i * 8);
				int num6 = (int)byte_0.smethod_3(Class5.Class6.int_4 + i * 8 + 4);
				int num7 = num5 + 8 * num2;
				int num8 = num6;
				array = array.smethod_0(((uint)num7).smethod_5());
				array = array.smethod_0(((uint)num8).smethod_5());
			}
			tuple = Class34.smethod_81(byte_1, int_2);
			int item9 = tuple.Item1;
			for (int j = 0; j < num2; j++)
			{
				tuple = Class34.smethod_81(byte_1, int_2 + j);
				int item10 = tuple.Item1;
				int num9 = item3 + (item10 - item9) + 8 * num2;
				int num10 = 2 * (int_0 + j);
				array = array.smethod_0(((uint)num9).smethod_5());
				array = array.smethod_0(((uint)num10).smethod_5());
			}
			int num11 = item8 - item5;
			for (int k = int_0; k < (int)num; k++)
			{
				int num12 = (int)byte_0.smethod_3(Class5.Class6.int_4 + k * 8);
				byte_0.smethod_3(Class5.Class6.int_4 + k * 8 + 4);
				int num13 = num12 + num11 + 8 * num2;
				int num14 = 2 * (k + num2);
				array = array.smethod_0(((uint)num13).smethod_5());
				array = array.smethod_0(((uint)num14).smethod_5());
			}
			int num15 = num3 - (Class5.Class6.int_4 + 8 * ((int)num + num2));
			if (num15 > 0)
			{
				byte[] array2 = Enumerable.Repeat<byte>(0, num15).ToArray<byte>();
				array = array.smethod_0(array2);
			}
			array = array.smethod_0(byte_0.smethod_6(item, item3));
			array = array.smethod_0(byte_1.smethod_6(item5, item8));
			return array.smethod_0(byte_0.smethod_6(item3, byte_0.Length));
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000882E File Offset: 0x00006A2E
		static bool smethod_136(Class27.Class29 class29_0)
		{
			return class29_0.int_0 == class29_0.int_1;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00036D80 File Offset: 0x00034F80
		static void smethod_137(Form1 form1_0, string string_0, int int_0)
		{
			int num = form1_0.list_2[int_0].method_4();
			int num2 = form1_0.list_2[int_0 + 1].method_4();
			int num3 = form1_0.method_16(int_0);
			if (num3 == 0)
			{
				return;
			}
			if (num3 == 2)
			{
				form1_0.method_17(string_0, int_0);
				return;
			}
			int num4 = 0;
			int num5 = 0;
			List<string> list = new List<string>(1000);
			int num6 = 0;
			string text = string.Join("\u3000", new string[form1_0.struct3_0.int_8 + 1]);
			for (int i = num; i < num2; i++)
			{
				if (form1_0.checkBox_2.Checked)
				{
					string text2 = Form1.list_0[i].Trim();
					if (text2 != "")
					{
						num5 = num5 + Class34.smethod_148(Form1.list_0[i]) + 19;
					}
				}
				else if (form1_0.checkBox_4.Checked)
				{
					num5 = num5 + Class34.smethod_148(text + Form1.list_0[i].TrimStart(new char[0])) + 19;
				}
				else
				{
					num5 = num5 + Class34.smethod_148(Form1.list_0[i]) + 19;
				}
				list.Add(Form1.list_0[i]);
				if (num5 > Form1.int_0 - 1000)
				{
					num5 = 0;
					num6 = form1_0.method_18(list, "chapter", int_0, num4);
					if (num6 == 0)
					{
						if (num4 == 0)
						{
							File.Delete(string.Format("{0}\\OEBPS\\chapter{1}.html", form1_0.string_1, int_0));
						}
						else
						{
							File.Delete(string.Format("{0}\\OEBPS\\chapter{1}-{2}.html", form1_0.string_1, int_0, num4));
						}
					}
					list.Clear();
					num4++;
				}
			}
			if (list.Count != 0)
			{
				num6 = form1_0.method_18(list, "chapter", int_0, num4);
			}
			if (num6 == 0)
			{
				if (num4 == 0)
				{
					File.Delete(string.Format("{0}\\OEBPS\\chapter{1}.html", form1_0.string_1, int_0));
					return;
				}
				File.Delete(string.Format("{0}\\OEBPS\\chapter{1}-{2}.html", form1_0.string_1, int_0, num4));
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000883E File Offset: 0x00006A3E
		static void smethod_138(Class18 class18_0)
		{
			class18_0.container_0 = new Container();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00036FA8 File Offset: 0x000351A8
		static ICryptoTransform smethod_139(byte[] byte_0, bool bool_0, byte[] byte_1)
		{
			ICryptoTransform cryptoTransform;
			using (SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged())
			{
				cryptoTransform = (bool_0 ? symmetricAlgorithm.CreateDecryptor(byte_1, byte_0) : symmetricAlgorithm.CreateEncryptor(byte_1, byte_0));
			}
			return cryptoTransform;
		}

		// Token: 0x06000681 RID: 1665
		[DllImport("user32.dll")]
		static extern IntPtr GetSystemMenu(IntPtr intptr_0, bool bool_0);

		// Token: 0x06000682 RID: 1666 RVA: 0x00036FF0 File Offset: 0x000351F0
		static void smethod_140(Form2 form2_0, global::System.Drawing.Color color_0)
		{
			if (!form2_0.bool_0)
			{
				return;
			}
			if (form2_0.class18_0.Items.Count <= 0)
			{
				return;
			}
			int i = 0;
			while (i < form2_0.list_0.Count - 1)
			{
				Class1 @class = form2_0.list_0[i];
				if (form2_0.list_0[i + 1].method_4() - form2_0.list_0[i].method_4() <= form2_0.numericUpDown_0.Value)
				{
					try
					{
						form2_0.class18_0.Items[i].ForeColor = color_0;
						goto IL_00D9;
					}
					catch (Exception)
					{
						goto IL_00D9;
					}
					goto Block_5;
				}
				goto IL_0097;
				IL_00D9:
				i++;
				continue;
				Block_5:
				try
				{
					IL_0097:
					if (form2_0.class18_0.Items[i].ForeColor == global::System.Drawing.Color.Firebrick)
					{
						form2_0.class18_0.Items[i].ForeColor = global::System.Drawing.Color.Black;
					}
				}
				catch (Exception)
				{
				}
				goto IL_00D9;
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000370FC File Offset: 0x000352FC
		static void smethod_141(Form1 form1_0)
		{
			form1_0.icontainer_0 = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form1));
			form1_0.textBox_0 = new TextBox();
			form1_0.textBox_1 = new TextBox();
			form1_0.textBox_2 = new TextBox();
			form1_0.label_0 = new Label();
			form1_0.label_1 = new Label();
			form1_0.label_2 = new Label();
			form1_0.label_3 = new Label();
			form1_0.label_4 = new Label();
			form1_0.comboBox_0 = new ComboBox();
			form1_0.label_5 = new Label();
			form1_0.tabControl_0 = new TabControl();
			form1_0.tabPage_3 = new TabPage();
			form1_0.button_10 = new Button();
			form1_0.button_11 = new Button();
			form1_0.radioButton_12 = new RadioButton();
			form1_0.comboBox_13 = new ComboBox();
			form1_0.comboBox_12 = new ComboBox();
			form1_0.label_17 = new Label();
			form1_0.comboBox_2 = new ComboBox();
			form1_0.comboBox_4 = new ComboBox();
			form1_0.label_7 = new Label();
			form1_0.numericUpDown_0 = new NumericUpDown();
			form1_0.radioButton_6 = new RadioButton();
			form1_0.button_3 = new Button();
			form1_0.radioButton_4 = new RadioButton();
			form1_0.radioButton_5 = new RadioButton();
			form1_0.comboBox_3 = new ComboBox();
			form1_0.checkBox_1 = new CheckBox();
			form1_0.tabPage_0 = new TabPage();
			form1_0.comboBox_24 = new ComboBox();
			form1_0.comboBox_25 = new ComboBox();
			form1_0.comboBox_26 = new ComboBox();
			form1_0.comboBox_23 = new ComboBox();
			form1_0.comboBox_20 = new ComboBox();
			form1_0.label_31 = new Label();
			form1_0.textBox_15 = new TextBox();
			form1_0.textBox_16 = new TextBox();
			form1_0.label_30 = new Label();
			form1_0.comboBox_14 = new ComboBox();
			form1_0.label_23 = new Label();
			form1_0.numericUpDown_1 = new NumericUpDown();
			form1_0.checkBox_2 = new CheckBox();
			form1_0.comboBox_10 = new ComboBox();
			form1_0.label_13 = new Label();
			form1_0.label_14 = new Label();
			form1_0.comboBox_11 = new ComboBox();
			form1_0.label_15 = new Label();
			form1_0.label_16 = new Label();
			form1_0.comboBox_9 = new ComboBox();
			form1_0.comboBox_6 = new ComboBox();
			form1_0.label_9 = new Label();
			form1_0.comboBox_7 = new ComboBox();
			form1_0.label_10 = new Label();
			form1_0.comboBox_18 = new ComboBox();
			form1_0.label_29 = new Label();
			form1_0.comboBox_8 = new ComboBox();
			form1_0.label_11 = new Label();
			form1_0.comboBox_5 = new ComboBox();
			form1_0.label_8 = new Label();
			form1_0.checkBox_4 = new CheckBox();
			form1_0.label_32 = new Label();
			form1_0.label_12 = new Label();
			form1_0.tabPage_1 = new TabPage();
			form1_0.textBox_5 = new TextBox();
			form1_0.checkBox_0 = new CheckBox();
			form1_0.radioButton_0 = new RadioButton();
			form1_0.button_0 = new Button();
			form1_0.textBox_3 = new TextBox();
			form1_0.radioButton_1 = new RadioButton();
			form1_0.radioButton_2 = new RadioButton();
			form1_0.comboBox_1 = new ComboBox();
			form1_0.radioButton_3 = new RadioButton();
			form1_0.tabPage_7 = new TabPage();
			form1_0.comboBox_22 = new ComboBox();
			form1_0.label_41 = new Label();
			form1_0.textBox_20 = new TextBox();
			form1_0.label_40 = new Label();
			form1_0.comboBox_21 = new ComboBox();
			form1_0.label_38 = new Label();
			form1_0.dateTimePicker_0 = new DateTimePicker();
			form1_0.label_39 = new Label();
			form1_0.label_37 = new Label();
			form1_0.textBox_17 = new TextBox();
			form1_0.label_34 = new Label();
			form1_0.textBox_18 = new TextBox();
			form1_0.label_35 = new Label();
			form1_0.textBox_19 = new TextBox();
			form1_0.label_36 = new Label();
			form1_0.tabPage_2 = new TabPage();
			form1_0.radioButton_9 = new RadioButton();
			form1_0.checkBox_5 = new CheckBox();
			form1_0.radioButton_7 = new RadioButton();
			form1_0.radioButton_8 = new RadioButton();
			form1_0.button_1 = new Button();
			form1_0.textBox_4 = new TextBox();
			form1_0.tabPage_4 = new TabPage();
			form1_0.listView_0 = new ListView();
			form1_0.columnHeader_0 = new ColumnHeader();
			form1_0.button_8 = new Button();
			form1_0.textBox_6 = new TextBox();
			form1_0.pictureBox_1 = new PictureBox();
			form1_0.tabPage_6 = new TabPage();
			form1_0.checkBox_13 = new CheckBox();
			form1_0.checkBox_12 = new CheckBox();
			form1_0.textBox_14 = new TextBox();
			form1_0.radioButton_11 = new RadioButton();
			form1_0.label_28 = new Label();
			form1_0.textBox_13 = new TextBox();
			form1_0.comboBox_17 = new ComboBox();
			form1_0.label_26 = new Label();
			form1_0.comboBox_16 = new ComboBox();
			form1_0.label_25 = new Label();
			form1_0.checkBox_10 = new CheckBox();
			form1_0.checkBox_11 = new CheckBox();
			form1_0.label_27 = new Label();
			form1_0.radioButton_10 = new RadioButton();
			form1_0.tabPage_5 = new TabPage();
			form1_0.checkBox_14 = new CheckBox();
			form1_0.comboBox_19 = new ComboBox();
			form1_0.comboBox_15 = new ComboBox();
			form1_0.checkBox_9 = new CheckBox();
			form1_0.button_9 = new Button();
			form1_0.checkBox_8 = new CheckBox();
			form1_0.checkBox_7 = new CheckBox();
			form1_0.textBox_11 = new TextBox();
			form1_0.textBox_7 = new TextBox();
			form1_0.label_19 = new Label();
			form1_0.textBox_8 = new TextBox();
			form1_0.label_20 = new Label();
			form1_0.textBox_9 = new TextBox();
			form1_0.textBox_10 = new TextBox();
			form1_0.checkBox_6 = new CheckBox();
			form1_0.label_24 = new Label();
			form1_0.label_18 = new Label();
			form1_0.label_21 = new Label();
			form1_0.label_22 = new Label();
			form1_0.label_33 = new Label();
			form1_0.linkLabel_0 = new LinkLabel();
			form1_0.label_6 = new Label();
			form1_0.button_2 = new Button();
			form1_0.openFileDialog_0 = new OpenFileDialog();
			form1_0.button_4 = new Button();
			form1_0.button_5 = new Button();
			form1_0.button_6 = new Button();
			form1_0.saveFileDialog_0 = new SaveFileDialog();
			form1_0.openFileDialog_1 = new OpenFileDialog();
			form1_0.button_7 = new Button();
			form1_0.openFileDialog_2 = new OpenFileDialog();
			form1_0.contextMenuStrip_0 = new ContextMenuStrip(form1_0.icontainer_0);
			form1_0.toolStripMenuItem_1 = new ToolStripMenuItem();
			form1_0.toolStripMenuItem_0 = new ToolStripMenuItem();
			form1_0.openFileDialog_3 = new OpenFileDialog();
			form1_0.openFileDialog_4 = new OpenFileDialog();
			form1_0.toolTip_0 = new ToolTip(form1_0.icontainer_0);
			form1_0.checkBox_3 = new CheckBox();
			form1_0.pictureBox_0 = new PictureBox();
			form1_0.textBox_12 = new TextBox();
			form1_0.folderBrowserDialog_0 = new FolderBrowserDialog();
			form1_0.comboBox_27 = new ComboBox();
			form1_0.saveFileDialog_1 = new SaveFileDialog();
			form1_0.openFileDialog_5 = new OpenFileDialog();
			form1_0.label_42 = new Label();
			form1_0.comboBox_28 = new ComboBox();
			form1_0.tabControl_0.SuspendLayout();
			form1_0.tabPage_3.SuspendLayout();
			((ISupportInitialize)form1_0.numericUpDown_0).BeginInit();
			form1_0.tabPage_0.SuspendLayout();
			((ISupportInitialize)form1_0.numericUpDown_1).BeginInit();
			form1_0.tabPage_1.SuspendLayout();
			form1_0.tabPage_7.SuspendLayout();
			form1_0.tabPage_2.SuspendLayout();
			form1_0.tabPage_4.SuspendLayout();
			((ISupportInitialize)form1_0.pictureBox_1).BeginInit();
			form1_0.tabPage_6.SuspendLayout();
			form1_0.tabPage_5.SuspendLayout();
			form1_0.contextMenuStrip_0.SuspendLayout();
			((ISupportInitialize)form1_0.pictureBox_0).BeginInit();
			form1_0.SuspendLayout();
			form1_0.textBox_0.AllowDrop = true;
			form1_0.textBox_0.BackColor = SystemColors.Info;
			form1_0.textBox_0.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			form1_0.textBox_0.ForeColor = global::System.Drawing.Color.DarkGray;
			form1_0.textBox_0.Location = new Point(49, 12);
			form1_0.textBox_0.Name = "textBox_InputFileName";
			form1_0.textBox_0.ReadOnly = true;
			form1_0.textBox_0.Size = new Size(285, 20);
			form1_0.textBox_0.TabIndex = 0;
			form1_0.textBox_0.TabStop = false;
			form1_0.textBox_0.Text = "txt转epub/mobi或epub转txt/mobi，拖放文件至此";
			form1_0.textBox_0.TextChanged += form1_0.method_0;
			form1_0.textBox_0.DragDrop += form1_0.method_2;
			form1_0.textBox_0.DragEnter += form1_0.method_1;
			form1_0.textBox_1.Location = new Point(251, 52);
			form1_0.textBox_1.Name = "textBox_AuthorName";
			form1_0.textBox_1.Size = new Size(109, 20);
			form1_0.textBox_1.TabIndex = 3;
			form1_0.textBox_2.Location = new Point(49, 52);
			form1_0.textBox_2.Name = "textBox_BookName";
			form1_0.textBox_2.Size = new Size(154, 20);
			form1_0.textBox_2.TabIndex = 2;
			form1_0.label_0.AutoSize = true;
			form1_0.label_0.Location = new Point(12, 16);
			form1_0.label_0.Name = "label_InputFileName";
			form1_0.label_0.Size = new Size(31, 13);
			form1_0.label_0.TabIndex = 9;
			form1_0.label_0.Text = "输入";
			form1_0.toolTip_0.SetToolTip(form1_0.label_0, "源文件，支持纯文本txt格式输入，\r\n转换到epub或者mobi格式\r\n或者epub输入，输出txt\r\n拖放至黄色框或者浏览选择\r\n*双击此标签直接定位到输入文件");
			form1_0.label_0.DoubleClick += form1_0.method_70;
			form1_0.label_1.AutoSize = true;
			form1_0.label_1.Location = new Point(12, 56);
			form1_0.label_1.Name = "label2";
			form1_0.label_1.Size = new Size(31, 13);
			form1_0.label_1.TabIndex = 10;
			form1_0.label_1.Text = "书名";
			form1_0.toolTip_0.SetToolTip(form1_0.label_1, "书名，必填");
			form1_0.label_2.AutoSize = true;
			form1_0.label_2.Location = new Point(209, 56);
			form1_0.label_2.Name = "label3";
			form1_0.label_2.Size = new Size(31, 13);
			form1_0.label_2.TabIndex = 11;
			form1_0.label_2.Text = "作者";
			form1_0.toolTip_0.SetToolTip(form1_0.label_2, "作者名，可选");
			form1_0.label_3.AutoSize = true;
			form1_0.label_3.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			form1_0.label_3.ForeColor = global::System.Drawing.Color.Black;
			form1_0.label_3.Location = new Point(15, 28);
			form1_0.label_3.Name = "label4";
			form1_0.label_3.Size = new Size(43, 15);
			form1_0.label_3.TabIndex = 0;
			form1_0.label_3.Text = "页边距";
			form1_0.toolTip_0.SetToolTip(form1_0.label_3, "设置上下左右页边距\r\n*当选项为“X”时，EasyPub将略过此设置，\r\n生成的电子书使用阅读器默认值");
			form1_0.label_4.AutoSize = true;
			form1_0.label_4.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			form1_0.label_4.ForeColor = global::System.Drawing.Color.Black;
			form1_0.label_4.Location = new Point(142, 70);
			form1_0.label_4.Name = "label5";
			form1_0.label_4.Size = new Size(31, 15);
			form1_0.label_4.TabIndex = 2;
			form1_0.label_4.Text = "行距";
			form1_0.toolTip_0.SetToolTip(form1_0.label_4, "行距，相对于当前字体大小的百分比\r\n*当选项为“X”时，EasyPub将略过此设置，\r\n生成的电子书使用阅读器默认值\r\n");
			form1_0.comboBox_0.FormattingEnabled = true;
			form1_0.comboBox_0.Items.AddRange(new object[]
			{
				"X", "100", "110", "120", "130", "140", "150", "160", "170", "180",
				"190", "200"
			});
			form1_0.comboBox_0.Location = new Point(174, 67);
			form1_0.comboBox_0.Name = "comboBox_LineHeight";
			form1_0.comboBox_0.Size = new Size(41, 21);
			form1_0.comboBox_0.TabIndex = 6;
			form1_0.comboBox_0.Text = "120";
			form1_0.comboBox_0.DropDown += form1_0.method_74;
			form1_0.comboBox_0.KeyPress += form1_0.method_48;
			form1_0.label_5.AutoSize = true;
			form1_0.label_5.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			form1_0.label_5.ForeColor = global::System.Drawing.Color.Black;
			form1_0.label_5.Location = new Point(229, 70);
			form1_0.label_5.Name = "label6";
			form1_0.label_5.Size = new Size(43, 15);
			form1_0.label_5.TabIndex = 4;
			form1_0.label_5.Text = "段间距";
			form1_0.toolTip_0.SetToolTip(form1_0.label_5, "每段之间的间距，单位可选px, pt或em\r\n*当选项为“X”时，EasyPub将略过此设置，\r\n生成的电子书使用阅读器默认值");
			form1_0.tabControl_0.Controls.Add(form1_0.tabPage_3);
			form1_0.tabControl_0.Controls.Add(form1_0.tabPage_0);
			form1_0.tabControl_0.Controls.Add(form1_0.tabPage_1);
			form1_0.tabControl_0.Controls.Add(form1_0.tabPage_7);
			form1_0.tabControl_0.Controls.Add(form1_0.tabPage_2);
			form1_0.tabControl_0.Controls.Add(form1_0.tabPage_4);
			form1_0.tabControl_0.Controls.Add(form1_0.tabPage_6);
			form1_0.tabControl_0.Controls.Add(form1_0.tabPage_5);
			form1_0.tabControl_0.Location = new Point(12, 157);
			form1_0.tabControl_0.Name = "tabControl_Main";
			form1_0.tabControl_0.SelectedIndex = 0;
			form1_0.tabControl_0.Size = new Size(460, 218);
			form1_0.tabControl_0.TabIndex = 12;
			form1_0.tabControl_0.DrawItem += form1_0.method_67;
			form1_0.tabPage_3.Controls.Add(form1_0.button_10);
			form1_0.tabPage_3.Controls.Add(form1_0.button_11);
			form1_0.tabPage_3.Controls.Add(form1_0.radioButton_12);
			form1_0.tabPage_3.Controls.Add(form1_0.comboBox_13);
			form1_0.tabPage_3.Controls.Add(form1_0.comboBox_12);
			form1_0.tabPage_3.Controls.Add(form1_0.label_17);
			form1_0.tabPage_3.Controls.Add(form1_0.comboBox_2);
			form1_0.tabPage_3.Controls.Add(form1_0.comboBox_4);
			form1_0.tabPage_3.Controls.Add(form1_0.label_7);
			form1_0.tabPage_3.Controls.Add(form1_0.numericUpDown_0);
			form1_0.tabPage_3.Controls.Add(form1_0.radioButton_6);
			form1_0.tabPage_3.Controls.Add(form1_0.button_3);
			form1_0.tabPage_3.Controls.Add(form1_0.radioButton_4);
			form1_0.tabPage_3.Controls.Add(form1_0.radioButton_5);
			form1_0.tabPage_3.Controls.Add(form1_0.comboBox_3);
			form1_0.tabPage_3.Controls.Add(form1_0.checkBox_1);
			form1_0.tabPage_3.Location = new Point(4, 22);
			form1_0.tabPage_3.Name = "tabPageChapter";
			form1_0.tabPage_3.Padding = new Padding(3);
			form1_0.tabPage_3.Size = new Size(452, 192);
			form1_0.tabPage_3.TabIndex = 4;
			form1_0.tabPage_3.Text = " 章节 ";
			form1_0.tabPage_3.UseVisualStyleBackColor = true;
			form1_0.button_10.Location = new Point(301, 149);
			form1_0.button_10.Name = "button_SaveSplitIndex";
			form1_0.button_10.Size = new Size(43, 26);
			form1_0.button_10.TabIndex = 103;
			form1_0.button_10.Text = "保存";
			form1_0.toolTip_0.SetToolTip(form1_0.button_10, "保存当前章节信息到文件");
			form1_0.button_10.UseVisualStyleBackColor = true;
			form1_0.button_10.Click += form1_0.method_71;
			form1_0.button_11.BackgroundImageLayout = ImageLayout.None;
			form1_0.button_11.Enabled = false;
			form1_0.button_11.Location = new Point(104, 152);
			form1_0.button_11.Name = "button_LoadEPSav";
			form1_0.button_11.Size = new Size(24, 21);
			form1_0.button_11.TabIndex = 102;
			form1_0.button_11.Text = "...";
			form1_0.toolTip_0.SetToolTip(form1_0.button_11, "选择*.epsav文件，加载章节信息");
			form1_0.button_11.UseVisualStyleBackColor = true;
			form1_0.button_11.Click += form1_0.method_72;
			form1_0.radioButton_12.AutoSize = true;
			form1_0.radioButton_12.ForeColor = SystemColors.ControlText;
			form1_0.radioButton_12.Location = new Point(13, 154);
			form1_0.radioButton_12.Name = "radioButton_LoadEPSav";
			form1_0.radioButton_12.Size = new Size(85, 17);
			form1_0.radioButton_12.TabIndex = 33;
			form1_0.radioButton_12.Text = "从文件加载";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_12, "从外部文件加载章节信息\r\n");
			form1_0.radioButton_12.UseVisualStyleBackColor = true;
			form1_0.radioButton_12.CheckedChanged += form1_0.method_73;
			form1_0.comboBox_13.FormattingEnabled = true;
			form1_0.comboBox_13.Location = new Point(104, 55);
			form1_0.comboBox_13.Name = "comboBox_simple_reg";
			form1_0.comboBox_13.Size = new Size(324, 21);
			form1_0.comboBox_13.TabIndex = 32;
			form1_0.comboBox_13.DropDown += form1_0.method_74;
			form1_0.comboBox_13.Enter += form1_0.method_46;
			form1_0.comboBox_12.FormattingEnabled = true;
			form1_0.comboBox_12.Location = new Point(104, 88);
			form1_0.comboBox_12.Name = "comboBox_full_reg";
			form1_0.comboBox_12.Size = new Size(324, 21);
			form1_0.comboBox_12.TabIndex = 31;
			form1_0.comboBox_12.DropDown += form1_0.method_74;
			form1_0.comboBox_12.Enter += form1_0.method_41;
			form1_0.label_17.AutoSize = true;
			form1_0.label_17.Location = new Point(31, 59);
			form1_0.label_17.Name = "label_SplitSimpleExt";
			form1_0.label_17.Size = new Size(55, 13);
			form1_0.label_17.TabIndex = 29;
			form1_0.label_17.Text = "附加规则";
			form1_0.comboBox_2.FormattingEnabled = true;
			form1_0.comboBox_2.Items.AddRange(new object[] { "第", "卷", "[第卷]" });
			form1_0.comboBox_2.Location = new Point(174, 22);
			form1_0.comboBox_2.Name = "comboBox_p1";
			form1_0.comboBox_2.Size = new Size(62, 21);
			form1_0.comboBox_2.TabIndex = 16;
			form1_0.comboBox_2.Text = "第";
			form1_0.comboBox_2.DropDown += form1_0.method_74;
			form1_0.comboBox_2.Enter += form1_0.method_42;
			form1_0.comboBox_4.FormattingEnabled = true;
			form1_0.comboBox_4.Items.AddRange(new object[] { "混合型数字", "纯中文数字", "纯阿拉伯数字" });
			form1_0.comboBox_4.Location = new Point(236, 22);
			form1_0.comboBox_4.Name = "comboBox_p2";
			form1_0.comboBox_4.Size = new Size(82, 21);
			form1_0.comboBox_4.TabIndex = 26;
			form1_0.comboBox_4.Text = "混合型数字";
			form1_0.comboBox_4.DropDown += form1_0.method_74;
			form1_0.comboBox_4.Enter += form1_0.method_43;
			form1_0.comboBox_4.KeyPress += form1_0.method_3;
			form1_0.label_7.AutoSize = true;
			form1_0.label_7.Location = new Point(146, 125);
			form1_0.label_7.Name = "label8";
			form1_0.label_7.Size = new Size(19, 13);
			form1_0.label_7.TabIndex = 25;
			form1_0.label_7.Text = "章";
			form1_0.numericUpDown_0.Location = new Point(104, 121);
			NumericUpDown numericUpDown_ = form1_0.numericUpDown_0;
			int[] array = new int[4];
			array[0] = 999;
			numericUpDown_.Maximum = new decimal(array);
			NumericUpDown numericUpDown_2 = form1_0.numericUpDown_0;
			int[] array2 = new int[4];
			array2[0] = 1;
			numericUpDown_2.Minimum = new decimal(array2);
			form1_0.numericUpDown_0.Name = "numericUpDown_SplitCount";
			form1_0.numericUpDown_0.Size = new Size(39, 20);
			form1_0.numericUpDown_0.TabIndex = 24;
			form1_0.numericUpDown_0.TextAlign = HorizontalAlignment.Right;
			NumericUpDown numericUpDown_3 = form1_0.numericUpDown_0;
			int[] array3 = new int[4];
			array3[0] = 10;
			numericUpDown_3.Value = new decimal(array3);
			form1_0.numericUpDown_0.Enter += form1_0.method_40;
			form1_0.radioButton_6.AutoSize = true;
			form1_0.radioButton_6.ForeColor = SystemColors.ControlText;
			form1_0.radioButton_6.Location = new Point(13, 123);
			form1_0.radioButton_6.Name = "radioButton_SplitLength";
			form1_0.radioButton_6.Size = new Size(85, 17);
			form1_0.radioButton_6.TabIndex = 23;
			form1_0.radioButton_6.Text = "按长度均分";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_6, "按长度均分\r\n当长度为1时将启用特殊的单章节模式:\r\n不生成目录页，不生成正文标题，模拟txt直读的效果 \r\n");
			form1_0.radioButton_6.UseVisualStyleBackColor = true;
			form1_0.button_3.Location = new Point(360, 149);
			form1_0.button_3.Name = "button_PreviewSplitResult";
			form1_0.button_3.Size = new Size(68, 26);
			form1_0.button_3.TabIndex = 22;
			form1_0.button_3.Text = "章节编辑";
			form1_0.toolTip_0.SetToolTip(form1_0.button_3, "打开章节编辑窗口\r\n删除章节，修改名称，调整层级");
			form1_0.button_3.UseVisualStyleBackColor = true;
			form1_0.button_3.Click += form1_0.method_10;
			form1_0.radioButton_4.AutoSize = true;
			form1_0.radioButton_4.Checked = true;
			form1_0.radioButton_4.ForeColor = SystemColors.ControlText;
			form1_0.radioButton_4.Location = new Point(13, 24);
			form1_0.radioButton_4.Name = "radioButton_SplitSimple";
			form1_0.radioButton_4.Size = new Size(73, 17);
			form1_0.radioButton_4.TabIndex = 14;
			form1_0.radioButton_4.TabStop = true;
			form1_0.radioButton_4.Text = "简易规则";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_4, "按EasyPub的预定设置分割章节 ");
			form1_0.radioButton_4.UseVisualStyleBackColor = true;
			form1_0.radioButton_5.AutoSize = true;
			form1_0.radioButton_5.ForeColor = SystemColors.ControlText;
			form1_0.radioButton_5.Location = new Point(13, 90);
			form1_0.radioButton_5.Name = "radioButton_SplitRegex";
			form1_0.radioButton_5.Size = new Size(85, 17);
			form1_0.radioButton_5.TabIndex = 15;
			form1_0.radioButton_5.Text = "正则表达式";
			form1_0.radioButton_5.UseVisualStyleBackColor = true;
			form1_0.comboBox_3.FormattingEnabled = true;
			form1_0.comboBox_3.Items.AddRange(new object[] { "章", "回", "卷", "节", "集", "部", "[章回卷节集部]" });
			form1_0.comboBox_3.Location = new Point(318, 22);
			form1_0.comboBox_3.Name = "comboBox_p3";
			form1_0.comboBox_3.Size = new Size(110, 21);
			form1_0.comboBox_3.TabIndex = 18;
			form1_0.comboBox_3.Text = "章";
			form1_0.comboBox_3.DropDown += form1_0.method_74;
			form1_0.comboBox_3.Enter += form1_0.method_44;
			form1_0.checkBox_1.AutoSize = true;
			form1_0.checkBox_1.Location = new Point(103, 24);
			form1_0.checkBox_1.Name = "checkBox_LeadingSpace";
			form1_0.checkBox_1.Size = new Size(74, 17);
			form1_0.checkBox_1.TabIndex = 27;
			form1_0.checkBox_1.Text = "行首空格";
			form1_0.checkBox_1.UseVisualStyleBackColor = true;
			form1_0.checkBox_1.Enter += form1_0.method_45;
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_24);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_25);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_26);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_23);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_20);
			form1_0.tabPage_0.Controls.Add(form1_0.label_31);
			form1_0.tabPage_0.Controls.Add(form1_0.textBox_15);
			form1_0.tabPage_0.Controls.Add(form1_0.textBox_16);
			form1_0.tabPage_0.Controls.Add(form1_0.label_30);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_14);
			form1_0.tabPage_0.Controls.Add(form1_0.label_23);
			form1_0.tabPage_0.Controls.Add(form1_0.numericUpDown_1);
			form1_0.tabPage_0.Controls.Add(form1_0.checkBox_2);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_10);
			form1_0.tabPage_0.Controls.Add(form1_0.label_13);
			form1_0.tabPage_0.Controls.Add(form1_0.label_14);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_11);
			form1_0.tabPage_0.Controls.Add(form1_0.label_15);
			form1_0.tabPage_0.Controls.Add(form1_0.label_16);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_9);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_6);
			form1_0.tabPage_0.Controls.Add(form1_0.label_9);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_7);
			form1_0.tabPage_0.Controls.Add(form1_0.label_10);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_18);
			form1_0.tabPage_0.Controls.Add(form1_0.label_29);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_8);
			form1_0.tabPage_0.Controls.Add(form1_0.label_11);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_5);
			form1_0.tabPage_0.Controls.Add(form1_0.label_8);
			form1_0.tabPage_0.Controls.Add(form1_0.label_3);
			form1_0.tabPage_0.Controls.Add(form1_0.label_5);
			form1_0.tabPage_0.Controls.Add(form1_0.label_4);
			form1_0.tabPage_0.Controls.Add(form1_0.comboBox_0);
			form1_0.tabPage_0.Controls.Add(form1_0.checkBox_4);
			form1_0.tabPage_0.Controls.Add(form1_0.label_32);
			form1_0.tabPage_0.Controls.Add(form1_0.label_12);
			form1_0.tabPage_0.Location = new Point(4, 22);
			form1_0.tabPage_0.Name = "tabPageLayout";
			form1_0.tabPage_0.Padding = new Padding(3);
			form1_0.tabPage_0.Size = new Size(452, 192);
			form1_0.tabPage_0.TabIndex = 0;
			form1_0.tabPage_0.Text = " 版式 ";
			form1_0.tabPage_0.UseVisualStyleBackColor = true;
			form1_0.comboBox_24.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_24.FormattingEnabled = true;
			form1_0.comboBox_24.Items.AddRange(new object[] { "px", "pt", "em", "%" });
			form1_0.comboBox_24.Location = new Point(402, 25);
			form1_0.comboBox_24.Name = "comboBox_PageRightUnit";
			form1_0.comboBox_24.Size = new Size(38, 21);
			form1_0.comboBox_24.TabIndex = 38;
			form1_0.comboBox_25.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_25.FormattingEnabled = true;
			form1_0.comboBox_25.Items.AddRange(new object[] { "px", "pt", "em", "%" });
			form1_0.comboBox_25.Location = new Point(307, 25);
			form1_0.comboBox_25.Name = "comboBox_PageLeftUnit";
			form1_0.comboBox_25.Size = new Size(38, 21);
			form1_0.comboBox_25.TabIndex = 37;
			form1_0.comboBox_26.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_26.FormattingEnabled = true;
			form1_0.comboBox_26.Items.AddRange(new object[] { "px", "pt", "em", "%" });
			form1_0.comboBox_26.Location = new Point(211, 25);
			form1_0.comboBox_26.Name = "comboBox_PageBottomUnit";
			form1_0.comboBox_26.Size = new Size(38, 21);
			form1_0.comboBox_26.TabIndex = 36;
			form1_0.comboBox_23.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_23.FormattingEnabled = true;
			form1_0.comboBox_23.Items.AddRange(new object[] { "px", "pt", "em", "%" });
			form1_0.comboBox_23.Location = new Point(116, 25);
			form1_0.comboBox_23.Name = "comboBox_PageTopUnit";
			form1_0.comboBox_23.Size = new Size(38, 21);
			form1_0.comboBox_23.TabIndex = 35;
			form1_0.comboBox_20.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_20.FormattingEnabled = true;
			form1_0.comboBox_20.Items.AddRange(new object[] { "px", "pt", "em" });
			form1_0.comboBox_20.Location = new Point(307, 67);
			form1_0.comboBox_20.Name = "comboBox_MarginTopUnit";
			form1_0.comboBox_20.Size = new Size(38, 21);
			form1_0.comboBox_20.TabIndex = 34;
			form1_0.label_31.AutoSize = true;
			form1_0.label_31.Location = new Point(291, 154);
			form1_0.label_31.Name = "label36";
			form1_0.label_31.Size = new Size(31, 13);
			form1_0.label_31.TabIndex = 33;
			form1_0.label_31.Text = "作者";
			form1_0.textBox_15.Location = new Point(322, 150);
			form1_0.textBox_15.Name = "textBox_AuthorFont";
			form1_0.textBox_15.Size = new Size(28, 20);
			form1_0.textBox_15.TabIndex = 15;
			form1_0.textBox_15.Text = "25";
			form1_0.textBox_15.KeyPress += form1_0.method_48;
			form1_0.textBox_16.Location = new Point(263, 150);
			form1_0.textBox_16.Name = "textBox_TitleFont";
			form1_0.textBox_16.Size = new Size(28, 20);
			form1_0.textBox_16.TabIndex = 14;
			form1_0.textBox_16.Text = "50";
			form1_0.textBox_16.KeyPress += form1_0.method_48;
			form1_0.label_30.AutoSize = true;
			form1_0.label_30.Location = new Point(155, 154);
			form1_0.label_30.Name = "label34";
			form1_0.label_30.Size = new Size(79, 13);
			form1_0.label_30.TabIndex = 29;
			form1_0.label_30.Text = "封面字体大小";
			form1_0.toolTip_0.SetToolTip(form1_0.label_30, "在使用“从书名生成封面图片”功能时\r\n书名和作者名的字体大小，单位em");
			form1_0.comboBox_14.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_14.FormattingEnabled = true;
			form1_0.comboBox_14.Items.AddRange(new object[] { "默认", "两端", "左", "右", "居中" });
			form1_0.comboBox_14.Location = new Point(393, 67);
			form1_0.comboBox_14.Name = "comboBox_TextAlign";
			form1_0.comboBox_14.Size = new Size(48, 21);
			form1_0.comboBox_14.TabIndex = 8;
			form1_0.label_23.AutoSize = true;
			form1_0.label_23.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			form1_0.label_23.ForeColor = global::System.Drawing.Color.Black;
			form1_0.label_23.Location = new Point(357, 70);
			form1_0.label_23.Name = "label23";
			form1_0.label_23.Size = new Size(31, 15);
			form1_0.label_23.TabIndex = 27;
			form1_0.label_23.Text = "对齐";
			form1_0.toolTip_0.SetToolTip(form1_0.label_23, "文字对齐方式\r\n默认：不处理对齐，由阅读器设定\r\n两端：justify方式\r\n左：left\r\n右：right\r\n居中：center");
			form1_0.numericUpDown_1.Location = new Point(253, 111);
			NumericUpDown numericUpDown_4 = form1_0.numericUpDown_1;
			int[] array4 = new int[4];
			array4[0] = 10;
			numericUpDown_4.Maximum = new decimal(array4);
			form1_0.numericUpDown_1.Name = "numericUpDown_AddSpace";
			form1_0.numericUpDown_1.Size = new Size(38, 20);
			form1_0.numericUpDown_1.TabIndex = 11;
			form1_0.numericUpDown_1.TextAlign = HorizontalAlignment.Center;
			form1_0.toolTip_0.SetToolTip(form1_0.numericUpDown_1, "全角空格的数目\r\n为0则每段开头无空格");
			NumericUpDown numericUpDown_5 = form1_0.numericUpDown_1;
			int[] array5 = new int[4];
			array5[0] = 2;
			numericUpDown_5.Value = new decimal(array5);
			form1_0.checkBox_2.AutoSize = true;
			form1_0.checkBox_2.ForeColor = global::System.Drawing.Color.Black;
			form1_0.checkBox_2.Location = new Point(312, 114);
			form1_0.checkBox_2.Name = "checkBox_removeblankline";
			form1_0.checkBox_2.Size = new Size(74, 17);
			form1_0.checkBox_2.TabIndex = 12;
			form1_0.checkBox_2.Text = "去除空行";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_2, "去除源txt文件中的空行");
			form1_0.checkBox_2.UseVisualStyleBackColor = true;
			form1_0.comboBox_10.FormattingEnabled = true;
			form1_0.comboBox_10.Items.AddRange(new object[]
			{
				"X", "80", "90", "100", "110", "120", "130", "140", "150", "160",
				"170", "180", "190", "200"
			});
			form1_0.comboBox_10.Location = new Point(79, 67);
			form1_0.comboBox_10.Name = "comboBox_Fontsize";
			form1_0.comboBox_10.Size = new Size(44, 21);
			form1_0.comboBox_10.TabIndex = 5;
			form1_0.comboBox_10.Text = "100";
			form1_0.comboBox_10.DropDown += form1_0.method_74;
			form1_0.comboBox_10.KeyPress += form1_0.method_48;
			form1_0.label_13.AutoSize = true;
			form1_0.label_13.Location = new Point(125, 71);
			form1_0.label_13.Name = "label21";
			form1_0.label_13.Size = new Size(15, 13);
			form1_0.label_13.TabIndex = 26;
			form1_0.label_13.Text = "%";
			form1_0.label_14.AutoSize = true;
			form1_0.label_14.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			form1_0.label_14.ForeColor = global::System.Drawing.Color.Black;
			form1_0.label_14.Location = new Point(15, 70);
			form1_0.label_14.Name = "label22";
			form1_0.label_14.Size = new Size(55, 15);
			form1_0.label_14.TabIndex = 24;
			form1_0.label_14.Text = "字体大小";
			form1_0.toolTip_0.SetToolTip(form1_0.label_14, "字体大小\r\n*当选项为“X”时，EasyPub将略过此设置，\r\n生成的电子书使用阅读器默认值");
			form1_0.comboBox_11.FormattingEnabled = true;
			form1_0.comboBox_11.Items.AddRange(new object[] { "X", "0", "1", "2", "3", "4" });
			form1_0.comboBox_11.Location = new Point(79, 109);
			form1_0.comboBox_11.Name = "comboBox_Indent";
			form1_0.comboBox_11.Size = new Size(37, 21);
			form1_0.comboBox_11.TabIndex = 9;
			form1_0.comboBox_11.Text = "0";
			form1_0.comboBox_11.DropDown += form1_0.method_74;
			form1_0.comboBox_11.KeyPress += form1_0.method_48;
			form1_0.label_15.AutoSize = true;
			form1_0.label_15.Location = new Point(118, 113);
			form1_0.label_15.Name = "label19";
			form1_0.label_15.Size = new Size(21, 13);
			form1_0.label_15.TabIndex = 22;
			form1_0.label_15.Text = "em";
			form1_0.label_16.AutoSize = true;
			form1_0.label_16.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			form1_0.label_16.ForeColor = global::System.Drawing.Color.Black;
			form1_0.label_16.Location = new Point(15, 112);
			form1_0.label_16.Name = "label20";
			form1_0.label_16.Size = new Size(55, 15);
			form1_0.label_16.TabIndex = 21;
			form1_0.label_16.Text = "行首缩进";
			form1_0.toolTip_0.SetToolTip(form1_0.label_16, "行首缩进效果\r\n注意有些阅读器不支持这个css属性\r\n（推荐）设为0并使用“全角空格缩进”功能\r\n*当选项为“X”时，EasyPub将略过此设置，\r\n生成的电子书使用阅读器默认值");
			form1_0.comboBox_9.FormattingEnabled = true;
			form1_0.comboBox_9.Items.AddRange(new object[]
			{
				"X", "0", "0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8",
				"0.9", "1", "2", "3", "4", "5", "6", "7", "8", "9",
				"10"
			});
			form1_0.comboBox_9.Location = new Point(271, 67);
			form1_0.comboBox_9.Name = "comboBox_MarginTop";
			form1_0.comboBox_9.Size = new Size(37, 21);
			form1_0.comboBox_9.TabIndex = 7;
			form1_0.comboBox_9.Text = "5";
			form1_0.comboBox_9.DropDown += form1_0.method_74;
			form1_0.comboBox_9.KeyPress += form1_0.method_48;
			form1_0.comboBox_6.FormattingEnabled = true;
			form1_0.comboBox_6.Items.AddRange(new object[]
			{
				"X", "0", "1", "2", "3", "4", "5", "6", "7", "8",
				"9", "10", "11", "12", "13", "14", "15", "16", "17", "18",
				"19", "20"
			});
			form1_0.comboBox_6.Location = new Point(365, 25);
			form1_0.comboBox_6.Name = "comboBox_PageRight";
			form1_0.comboBox_6.Size = new Size(37, 21);
			form1_0.comboBox_6.TabIndex = 4;
			form1_0.comboBox_6.Text = "0";
			form1_0.comboBox_6.DropDown += form1_0.method_74;
			form1_0.comboBox_6.KeyPress += form1_0.method_48;
			form1_0.label_9.AutoSize = true;
			form1_0.label_9.Location = new Point(348, 29);
			form1_0.label_9.Name = "label16";
			form1_0.label_9.Size = new Size(19, 13);
			form1_0.label_9.TabIndex = 15;
			form1_0.label_9.Text = "右";
			form1_0.comboBox_7.FormattingEnabled = true;
			form1_0.comboBox_7.Items.AddRange(new object[]
			{
				"X", "0", "1", "2", "3", "4", "5", "6", "7", "8",
				"9", "10", "11", "12", "13", "14", "15", "16", "17", "18",
				"19", "20"
			});
			form1_0.comboBox_7.Location = new Point(270, 25);
			form1_0.comboBox_7.Name = "comboBox_PageLeft";
			form1_0.comboBox_7.Size = new Size(37, 21);
			form1_0.comboBox_7.TabIndex = 3;
			form1_0.comboBox_7.Text = "0";
			form1_0.comboBox_7.DropDown += form1_0.method_74;
			form1_0.comboBox_7.KeyPress += form1_0.method_48;
			form1_0.label_10.AutoSize = true;
			form1_0.label_10.Location = new Point(253, 29);
			form1_0.label_10.Name = "label14";
			form1_0.label_10.Size = new Size(19, 13);
			form1_0.label_10.TabIndex = 12;
			form1_0.label_10.Text = "左";
			form1_0.comboBox_18.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_18.FormattingEnabled = true;
			form1_0.comboBox_18.Items.AddRange(new object[] { "一", "二", "三" });
			form1_0.comboBox_18.Location = new Point(96, 150);
			form1_0.comboBox_18.Name = "comboBox_CoverStyle";
			form1_0.comboBox_18.Size = new Size(35, 21);
			form1_0.comboBox_18.TabIndex = 13;
			form1_0.label_29.AutoSize = true;
			form1_0.label_29.Location = new Point(15, 154);
			form1_0.label_29.Name = "label25";
			form1_0.label_29.Size = new Size(79, 13);
			form1_0.label_29.TabIndex = 14;
			form1_0.label_29.Text = "封面图片样式";
			form1_0.toolTip_0.SetToolTip(form1_0.label_29, "使用不同方式实现图片居中\r\n请根据喜好/兼容性自行选择\r\n样式一：居中，此后图片大小固定（默认方式）\r\n样式二：另外一种居中方式，图片大小随屏幕尺寸变化\r\n样式三：使用svg居中");
			form1_0.comboBox_8.FormattingEnabled = true;
			form1_0.comboBox_8.Items.AddRange(new object[]
			{
				"X", "0", "1", "2", "3", "4", "5", "6", "7", "8",
				"9", "10", "11", "12", "13", "14", "15", "16", "17", "18",
				"19", "20"
			});
			form1_0.comboBox_8.Location = new Point(174, 25);
			form1_0.comboBox_8.Name = "comboBox_PageBottom";
			form1_0.comboBox_8.Size = new Size(37, 21);
			form1_0.comboBox_8.TabIndex = 2;
			form1_0.comboBox_8.Text = "0";
			form1_0.comboBox_8.DropDown += form1_0.method_74;
			form1_0.comboBox_8.KeyPress += form1_0.method_48;
			form1_0.label_11.AutoSize = true;
			form1_0.label_11.Location = new Point(158, 29);
			form1_0.label_11.Name = "label12";
			form1_0.label_11.Size = new Size(19, 13);
			form1_0.label_11.TabIndex = 9;
			form1_0.label_11.Text = "下";
			form1_0.comboBox_5.FormattingEnabled = true;
			form1_0.comboBox_5.Items.AddRange(new object[]
			{
				"X", "0", "1", "2", "3", "4", "5", "6", "7", "8",
				"9", "10", "11", "12", "13", "14", "15", "16", "17", "18",
				"19", "20"
			});
			form1_0.comboBox_5.Location = new Point(79, 25);
			form1_0.comboBox_5.Name = "comboBox_PageTop";
			form1_0.comboBox_5.Size = new Size(37, 21);
			form1_0.comboBox_5.TabIndex = 1;
			form1_0.comboBox_5.Text = "0";
			form1_0.comboBox_5.DropDown += form1_0.method_74;
			form1_0.comboBox_5.KeyPress += form1_0.method_48;
			form1_0.label_8.AutoSize = true;
			form1_0.label_8.Location = new Point(60, 29);
			form1_0.label_8.Name = "label9";
			form1_0.label_8.Size = new Size(19, 13);
			form1_0.label_8.TabIndex = 6;
			form1_0.label_8.Text = "上";
			form1_0.checkBox_4.AutoSize = true;
			form1_0.checkBox_4.ForeColor = global::System.Drawing.Color.Black;
			form1_0.checkBox_4.Location = new Point(154, 112);
			form1_0.checkBox_4.Name = "checkBox_AddSpace";
			form1_0.checkBox_4.Size = new Size(103, 17);
			form1_0.checkBox_4.TabIndex = 10;
			form1_0.checkBox_4.Text = "全角空格缩进x";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_4, "不勾选则保留原文行首空格缩进\r\n注意很多阅读器不支持半角空格缩进效果\r\n（推荐）勾选此项，使用全角空格缩进 \r\n可也设定此项数值为0，完全去除缩进\r\n*当使用此功能时请同时设置“行首缩进”为0em \r\n");
			form1_0.checkBox_4.UseVisualStyleBackColor = true;
			form1_0.label_32.AutoSize = true;
			form1_0.label_32.Location = new Point(232, 154);
			form1_0.label_32.Name = "label35";
			form1_0.label_32.Size = new Size(31, 13);
			form1_0.label_32.TabIndex = 32;
			form1_0.label_32.Text = "书名";
			form1_0.label_12.AutoSize = true;
			form1_0.label_12.Location = new Point(214, 71);
			form1_0.label_12.Name = "label17";
			form1_0.label_12.Size = new Size(15, 13);
			form1_0.label_12.TabIndex = 18;
			form1_0.label_12.Text = "%";
			form1_0.tabPage_1.Controls.Add(form1_0.textBox_5);
			form1_0.tabPage_1.Controls.Add(form1_0.checkBox_0);
			form1_0.tabPage_1.Controls.Add(form1_0.radioButton_0);
			form1_0.tabPage_1.Controls.Add(form1_0.button_0);
			form1_0.tabPage_1.Controls.Add(form1_0.textBox_3);
			form1_0.tabPage_1.Controls.Add(form1_0.radioButton_1);
			form1_0.tabPage_1.Controls.Add(form1_0.radioButton_2);
			form1_0.tabPage_1.Controls.Add(form1_0.comboBox_1);
			form1_0.tabPage_1.Controls.Add(form1_0.radioButton_3);
			form1_0.tabPage_1.Location = new Point(4, 22);
			form1_0.tabPage_1.Name = "tabPageFont";
			form1_0.tabPage_1.Size = new Size(452, 192);
			form1_0.tabPage_1.TabIndex = 2;
			form1_0.tabPage_1.Text = " 字体 ";
			form1_0.tabPage_1.UseVisualStyleBackColor = true;
			form1_0.textBox_5.Location = new Point(101, 65);
			form1_0.textBox_5.Name = "textBox_font_customized";
			form1_0.textBox_5.Size = new Size(297, 20);
			form1_0.textBox_5.TabIndex = 9;
			form1_0.checkBox_0.AutoSize = true;
			form1_0.checkBox_0.Location = new Point(312, 110);
			form1_0.checkBox_0.Name = "checkBox_font_subsetting";
			form1_0.checkBox_0.Size = new Size(86, 17);
			form1_0.checkBox_0.TabIndex = 8;
			form1_0.checkBox_0.Text = "仅嵌入子集";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_0, "减小内嵌字体的体积（推荐使用）");
			form1_0.checkBox_0.UseVisualStyleBackColor = true;
			form1_0.radioButton_0.AutoSize = true;
			form1_0.radioButton_0.Location = new Point(18, 153);
			form1_0.radioButton_0.Name = "radioButton_font_nothing";
			form1_0.radioButton_0.Size = new Size(133, 17);
			form1_0.radioButton_0.TabIndex = 7;
			form1_0.radioButton_0.Text = "使用阅读器默认字体";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_0, "不附加任何字体定义\r\n高级用户选用");
			form1_0.radioButton_0.UseVisualStyleBackColor = true;
			form1_0.radioButton_0.CheckedChanged += form1_0.method_33;
			form1_0.button_0.Location = new Point(278, 107);
			form1_0.button_0.Name = "button_embedded_select";
			form1_0.button_0.Size = new Size(24, 20);
			form1_0.button_0.TabIndex = 6;
			form1_0.button_0.Text = "...";
			form1_0.toolTip_0.SetToolTip(form1_0.button_0, "点击选择ttf字体文件");
			form1_0.button_0.UseVisualStyleBackColor = true;
			form1_0.button_0.Click += form1_0.method_34;
			form1_0.textBox_3.Location = new Point(101, 107);
			form1_0.textBox_3.Name = "textBox_font_embedded";
			form1_0.textBox_3.Size = new Size(174, 20);
			form1_0.textBox_3.TabIndex = 5;
			form1_0.radioButton_1.AutoSize = true;
			form1_0.radioButton_1.Location = new Point(18, 110);
			form1_0.radioButton_1.Name = "radioButton_font_embedded";
			form1_0.radioButton_1.Size = new Size(73, 17);
			form1_0.radioButton_1.TabIndex = 4;
			form1_0.radioButton_1.Text = "内嵌字体";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_1, "ePub文件内嵌ttf字体\r\n适于未汉化的阅读器");
			form1_0.radioButton_1.UseVisualStyleBackColor = true;
			form1_0.radioButton_1.CheckedChanged += form1_0.method_32;
			form1_0.radioButton_2.AutoSize = true;
			form1_0.radioButton_2.Location = new Point(18, 67);
			form1_0.radioButton_2.Name = "radioButton_font_userinput";
			form1_0.radioButton_2.Size = new Size(61, 17);
			form1_0.radioButton_2.TabIndex = 2;
			form1_0.radioButton_2.Text = "自定义";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_2, "外挂ttf字体显示中文\r\n直接输入中文字体的位置\r\n格式一般为 res:// + 字体绝对unix路径\r\n例如Android系统中文字体位置：\r\n/system/fonts/DroidSansFallback.ttf\r\n则输入\r\nres:///system/fonts/DroidSansFallback.ttf");
			form1_0.radioButton_2.UseVisualStyleBackColor = true;
			form1_0.radioButton_2.CheckedChanged += form1_0.method_31;
			form1_0.comboBox_1.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_1.FormattingEnabled = true;
			form1_0.comboBox_1.Location = new Point(101, 22);
			form1_0.comboBox_1.Name = "comboBox_fontpredefined";
			form1_0.comboBox_1.Size = new Size(298, 21);
			form1_0.comboBox_1.TabIndex = 1;
			form1_0.radioButton_3.AutoSize = true;
			form1_0.radioButton_3.Checked = true;
			form1_0.radioButton_3.Location = new Point(18, 24);
			form1_0.radioButton_3.Name = "radioButton_font_predefined";
			form1_0.radioButton_3.Size = new Size(73, 17);
			form1_0.radioButton_3.TabIndex = 0;
			form1_0.radioButton_3.TabStop = true;
			form1_0.radioButton_3.Text = "机型预设";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_3, "外挂ttf字体显示中文\r\n可自行修改\r\n");
			form1_0.radioButton_3.UseVisualStyleBackColor = true;
			form1_0.radioButton_3.CheckedChanged += form1_0.method_30;
			form1_0.tabPage_7.Controls.Add(form1_0.comboBox_22);
			form1_0.tabPage_7.Controls.Add(form1_0.label_41);
			form1_0.tabPage_7.Controls.Add(form1_0.textBox_20);
			form1_0.tabPage_7.Controls.Add(form1_0.label_40);
			form1_0.tabPage_7.Controls.Add(form1_0.comboBox_21);
			form1_0.tabPage_7.Controls.Add(form1_0.label_38);
			form1_0.tabPage_7.Controls.Add(form1_0.dateTimePicker_0);
			form1_0.tabPage_7.Controls.Add(form1_0.label_39);
			form1_0.tabPage_7.Controls.Add(form1_0.label_37);
			form1_0.tabPage_7.Controls.Add(form1_0.textBox_17);
			form1_0.tabPage_7.Controls.Add(form1_0.label_34);
			form1_0.tabPage_7.Controls.Add(form1_0.textBox_18);
			form1_0.tabPage_7.Controls.Add(form1_0.label_35);
			form1_0.tabPage_7.Controls.Add(form1_0.textBox_19);
			form1_0.tabPage_7.Controls.Add(form1_0.label_36);
			form1_0.tabPage_7.Location = new Point(4, 22);
			form1_0.tabPage_7.Name = "tabPageBookInfo";
			form1_0.tabPage_7.Padding = new Padding(3);
			form1_0.tabPage_7.Size = new Size(452, 192);
			form1_0.tabPage_7.TabIndex = 8;
			form1_0.tabPage_7.Text = "书籍信息";
			form1_0.tabPage_7.UseVisualStyleBackColor = true;
			form1_0.comboBox_22.FormattingEnabled = true;
			form1_0.comboBox_22.Items.AddRange(new object[] { "小说", "历史", "传记", "新闻", "财经", "其它" });
			form1_0.comboBox_22.Location = new Point(196, 58);
			form1_0.comboBox_22.Name = "comboBox_Subject";
			form1_0.comboBox_22.Size = new Size(86, 21);
			form1_0.comboBox_22.TabIndex = 5;
			form1_0.label_41.AutoSize = true;
			form1_0.label_41.ForeColor = global::System.Drawing.Color.Maroon;
			form1_0.label_41.Location = new Point(313, 169);
			form1_0.label_41.Name = "label45";
			form1_0.label_41.Size = new Size(127, 13);
			form1_0.label_41.TabIndex = 107;
			form1_0.label_41.Text = "本页内容全部为可选项";
			form1_0.toolTip_0.SetToolTip(form1_0.label_41, "全部内容可选，退出时不保留设置");
			form1_0.textBox_20.Location = new Point(56, 97);
			form1_0.textBox_20.Multiline = true;
			form1_0.textBox_20.Name = "textBox_Description";
			form1_0.textBox_20.Size = new Size(226, 85);
			form1_0.textBox_20.TabIndex = 7;
			form1_0.textBox_20.KeyDown += form1_0.method_66;
			form1_0.label_40.AutoSize = true;
			form1_0.label_40.Location = new Point(19, 100);
			form1_0.label_40.Name = "label43";
			form1_0.label_40.Size = new Size(31, 13);
			form1_0.label_40.TabIndex = 105;
			form1_0.label_40.Text = "简介";
			form1_0.toolTip_0.SetToolTip(form1_0.label_40, "作品简介");
			form1_0.comboBox_21.FormattingEnabled = true;
			form1_0.comboBox_21.Items.AddRange(new object[] { "zh-CN", "zh-TW", "en-US" });
			form1_0.comboBox_21.Location = new Point(345, 58);
			form1_0.comboBox_21.Name = "comboBox_Language";
			form1_0.comboBox_21.Size = new Size(86, 21);
			form1_0.comboBox_21.TabIndex = 6;
			form1_0.comboBox_21.Text = "zh-CN";
			form1_0.label_38.AutoSize = true;
			form1_0.label_38.Location = new Point(301, 62);
			form1_0.label_38.Name = "label44";
			form1_0.label_38.Size = new Size(31, 13);
			form1_0.label_38.TabIndex = 103;
			form1_0.label_38.Text = "语言";
			form1_0.toolTip_0.SetToolTip(form1_0.label_38, "书籍语言\r\nzh-CN：中文简体（默认）\r\nzh-TW：中文繁体\r\nen-US：英文\r\n\r\n**也可自行输入语言代码。\r\n不当设置可能导致错误");
			form1_0.dateTimePicker_0.CustomFormat = "yyyy-MM-dd";
			form1_0.dateTimePicker_0.Format = DateTimePickerFormat.Custom;
			form1_0.dateTimePicker_0.Location = new Point(346, 20);
			form1_0.dateTimePicker_0.Name = "dateTimePicker_Date";
			form1_0.dateTimePicker_0.Size = new Size(86, 20);
			form1_0.dateTimePicker_0.TabIndex = 3;
			form1_0.label_39.AutoSize = true;
			form1_0.label_39.Location = new Point(290, 24);
			form1_0.label_39.Name = "label38";
			form1_0.label_39.Size = new Size(55, 13);
			form1_0.label_39.TabIndex = 101;
			form1_0.label_39.Text = "出版日期";
			form1_0.toolTip_0.SetToolTip(form1_0.label_39, "出版日期，可选。默认为当前日期\r\n当输出非期刊格式时，仅年份有效");
			form1_0.label_37.AutoSize = true;
			form1_0.label_37.Location = new Point(156, 62);
			form1_0.label_37.Name = "label42";
			form1_0.label_37.Size = new Size(31, 13);
			form1_0.label_37.TabIndex = 6;
			form1_0.label_37.Text = "类别";
			form1_0.toolTip_0.SetToolTip(form1_0.label_37, "书籍分类");
			form1_0.textBox_17.Location = new Point(196, 20);
			form1_0.textBox_17.Name = "textBox_ISBN";
			form1_0.textBox_17.Size = new Size(86, 20);
			form1_0.textBox_17.TabIndex = 2;
			form1_0.label_34.AutoSize = true;
			form1_0.label_34.Location = new Point(155, 24);
			form1_0.label_34.Name = "label41";
			form1_0.label_34.Size = new Size(32, 13);
			form1_0.label_34.TabIndex = 4;
			form1_0.label_34.Text = "ISBN";
			form1_0.toolTip_0.SetToolTip(form1_0.label_34, "ISBN编号");
			form1_0.textBox_18.Location = new Point(56, 58);
			form1_0.textBox_18.Name = "textBox_Publisher";
			form1_0.textBox_18.Size = new Size(86, 20);
			form1_0.textBox_18.TabIndex = 4;
			form1_0.label_35.AutoSize = true;
			form1_0.label_35.Location = new Point(9, 62);
			form1_0.label_35.Name = "label40";
			form1_0.label_35.Size = new Size(43, 13);
			form1_0.label_35.TabIndex = 2;
			form1_0.label_35.Text = "出版社";
			form1_0.toolTip_0.SetToolTip(form1_0.label_35, "出版社");
			form1_0.textBox_19.Location = new Point(56, 20);
			form1_0.textBox_19.Name = "textBox_Translator";
			form1_0.textBox_19.Size = new Size(86, 20);
			form1_0.textBox_19.TabIndex = 1;
			form1_0.label_36.AutoSize = true;
			form1_0.label_36.Location = new Point(19, 24);
			form1_0.label_36.Name = "label39";
			form1_0.label_36.Size = new Size(31, 13);
			form1_0.label_36.TabIndex = 0;
			form1_0.label_36.Text = "译者";
			form1_0.toolTip_0.SetToolTip(form1_0.label_36, "翻译书籍译者");
			form1_0.tabPage_2.Controls.Add(form1_0.radioButton_9);
			form1_0.tabPage_2.Controls.Add(form1_0.checkBox_5);
			form1_0.tabPage_2.Controls.Add(form1_0.radioButton_7);
			form1_0.tabPage_2.Controls.Add(form1_0.radioButton_8);
			form1_0.tabPage_2.Controls.Add(form1_0.button_1);
			form1_0.tabPage_2.Controls.Add(form1_0.textBox_4);
			form1_0.tabPage_2.Location = new Point(4, 22);
			form1_0.tabPage_2.Name = "tabPageCss";
			form1_0.tabPage_2.Size = new Size(452, 192);
			form1_0.tabPage_2.TabIndex = 1;
			form1_0.tabPage_2.Text = "定制css";
			form1_0.tabPage_2.UseVisualStyleBackColor = true;
			form1_0.radioButton_9.AutoSize = true;
			form1_0.radioButton_9.Checked = true;
			form1_0.radioButton_9.Location = new Point(14, 159);
			form1_0.radioButton_9.Name = "radioButton_cssignore";
			form1_0.radioButton_9.Size = new Size(49, 17);
			form1_0.radioButton_9.TabIndex = 1;
			form1_0.radioButton_9.TabStop = true;
			form1_0.radioButton_9.Text = "略过";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_9, "仅用EasyPub自动生成的css\r\n完全不使用定制css的内容\r\n");
			form1_0.radioButton_9.UseVisualStyleBackColor = true;
			form1_0.checkBox_5.AutoSize = true;
			form1_0.checkBox_5.Location = new Point(209, 159);
			form1_0.checkBox_5.Name = "checkBox_SaveCss";
			form1_0.checkBox_5.Size = new Size(114, 17);
			form1_0.checkBox_5.TabIndex = 4;
			form1_0.checkBox_5.Text = "自动保存定制css";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_5, "保存当前css内容，下次运行自动加载");
			form1_0.checkBox_5.UseVisualStyleBackColor = true;
			form1_0.radioButton_7.AutoSize = true;
			form1_0.radioButton_7.Location = new Point(118, 159);
			form1_0.radioButton_7.Name = "radioButton_cssoverwrite";
			form1_0.radioButton_7.Size = new Size(49, 17);
			form1_0.radioButton_7.TabIndex = 3;
			form1_0.radioButton_7.Text = "覆盖";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_7, "EasyPub不再生成css\r\n完全仅使用定制css");
			form1_0.radioButton_7.UseVisualStyleBackColor = true;
			form1_0.radioButton_8.AutoSize = true;
			form1_0.radioButton_8.Location = new Point(63, 159);
			form1_0.radioButton_8.Name = "radioButton_cssappend";
			form1_0.radioButton_8.Size = new Size(49, 17);
			form1_0.radioButton_8.TabIndex = 2;
			form1_0.radioButton_8.Text = "追加";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_8, "定制css附于EasyPub自动生成的css之后\r\n定制css会有更高的优先级，补充或修改默认css设置");
			form1_0.radioButton_8.UseVisualStyleBackColor = true;
			form1_0.button_1.Location = new Point(332, 154);
			form1_0.button_1.Name = "button_OpenCSS";
			form1_0.button_1.Size = new Size(86, 27);
			form1_0.button_1.TabIndex = 5;
			form1_0.button_1.Text = "加载css文件";
			form1_0.toolTip_0.SetToolTip(form1_0.button_1, "从外部css文件中加载内容\r\n也可直接拖放css文件到上面的文本框");
			form1_0.button_1.UseVisualStyleBackColor = true;
			form1_0.button_1.Click += form1_0.method_35;
			form1_0.textBox_4.AllowDrop = true;
			form1_0.textBox_4.Location = new Point(14, 22);
			form1_0.textBox_4.Multiline = true;
			form1_0.textBox_4.Name = "textBox_cssfile";
			form1_0.textBox_4.ScrollBars = ScrollBars.Both;
			form1_0.textBox_4.Size = new Size(404, 122);
			form1_0.textBox_4.TabIndex = 0;
			form1_0.textBox_4.DragDrop += form1_0.method_37;
			form1_0.textBox_4.DragEnter += form1_0.method_36;
			form1_0.textBox_4.KeyDown += form1_0.method_47;
			form1_0.tabPage_4.Controls.Add(form1_0.listView_0);
			form1_0.tabPage_4.Controls.Add(form1_0.button_8);
			form1_0.tabPage_4.Controls.Add(form1_0.textBox_6);
			form1_0.tabPage_4.Controls.Add(form1_0.pictureBox_1);
			form1_0.tabPage_4.ForeColor = SystemColors.ControlText;
			form1_0.tabPage_4.Location = new Point(4, 22);
			form1_0.tabPage_4.Name = "tabPagePic";
			form1_0.tabPage_4.Padding = new Padding(3);
			form1_0.tabPage_4.Size = new Size(452, 192);
			form1_0.tabPage_4.TabIndex = 5;
			form1_0.tabPage_4.Text = " 插图 ";
			form1_0.tabPage_4.UseVisualStyleBackColor = true;
			form1_0.listView_0.AllowDrop = true;
			form1_0.listView_0.Columns.AddRange(new ColumnHeader[] { form1_0.columnHeader_0 });
			form1_0.listView_0.GridLines = true;
			form1_0.listView_0.Location = new Point(12, 10);
			form1_0.listView_0.Name = "listView_Images";
			form1_0.listView_0.ShowItemToolTips = true;
			form1_0.listView_0.Size = new Size(322, 147);
			form1_0.listView_0.TabIndex = 4;
			form1_0.toolTip_0.SetToolTip(form1_0.listView_0, "拖放图片文件至此。\r\n支持jpg/jpeg/gif/png格式。不支持bmp格式。\r\n请使用英文文件名，文件名不能相同！\r\n选择文件显示对应的html标签");
			form1_0.listView_0.UseCompatibleStateImageBehavior = false;
			form1_0.listView_0.View = View.Details;
			form1_0.listView_0.SelectedIndexChanged += form1_0.method_56;
			form1_0.listView_0.DragDrop += form1_0.method_53;
			form1_0.listView_0.DragEnter += form1_0.method_52;
			form1_0.listView_0.KeyDown += form1_0.method_55;
			form1_0.listView_0.MouseDoubleClick += form1_0.method_54;
			form1_0.columnHeader_0.Text = "拖放图片/目录至此。双击查看，Del删除。文件不能重名";
			form1_0.columnHeader_0.Width = 317;
			form1_0.button_8.Location = new Point(347, 133);
			form1_0.button_8.Name = "button_CopyToClipboard";
			form1_0.button_8.Size = new Size(92, 25);
			form1_0.button_8.TabIndex = 3;
			form1_0.button_8.Text = "复制到剪贴板";
			form1_0.toolTip_0.SetToolTip(form1_0.button_8, "复制自动生成的html到剪贴板");
			form1_0.button_8.UseVisualStyleBackColor = true;
			form1_0.button_8.Click += form1_0.method_58;
			form1_0.textBox_6.Location = new Point(12, 164);
			form1_0.textBox_6.Name = "textBox_HTMLRawCode";
			form1_0.textBox_6.Size = new Size(427, 20);
			form1_0.textBox_6.TabIndex = 2;
			form1_0.pictureBox_1.BorderStyle = BorderStyle.FixedSingle;
			form1_0.pictureBox_1.Location = new Point(347, 9);
			form1_0.pictureBox_1.Name = "pictureBox_Preview";
			form1_0.pictureBox_1.Size = new Size(93, 107);
			form1_0.pictureBox_1.SizeMode = PictureBoxSizeMode.StretchImage;
			form1_0.pictureBox_1.TabIndex = 1;
			form1_0.pictureBox_1.TabStop = false;
			form1_0.toolTip_0.SetToolTip(form1_0.pictureBox_1, "图片预览\r\n双击使用外部程序查看选中的图片");
			form1_0.pictureBox_1.DoubleClick += form1_0.method_59;
			form1_0.tabPage_6.Controls.Add(form1_0.comboBox_28);
			form1_0.tabPage_6.Controls.Add(form1_0.label_42);
			form1_0.tabPage_6.Controls.Add(form1_0.checkBox_13);
			form1_0.tabPage_6.Controls.Add(form1_0.checkBox_12);
			form1_0.tabPage_6.Controls.Add(form1_0.textBox_14);
			form1_0.tabPage_6.Controls.Add(form1_0.radioButton_11);
			form1_0.tabPage_6.Controls.Add(form1_0.label_28);
			form1_0.tabPage_6.Controls.Add(form1_0.textBox_13);
			form1_0.tabPage_6.Controls.Add(form1_0.comboBox_17);
			form1_0.tabPage_6.Controls.Add(form1_0.label_26);
			form1_0.tabPage_6.Controls.Add(form1_0.comboBox_16);
			form1_0.tabPage_6.Controls.Add(form1_0.label_25);
			form1_0.tabPage_6.Controls.Add(form1_0.checkBox_10);
			form1_0.tabPage_6.Controls.Add(form1_0.checkBox_11);
			form1_0.tabPage_6.Controls.Add(form1_0.label_27);
			form1_0.tabPage_6.Controls.Add(form1_0.radioButton_10);
			form1_0.tabPage_6.Location = new Point(4, 22);
			form1_0.tabPage_6.Name = "tabPageMobi";
			form1_0.tabPage_6.Padding = new Padding(3);
			form1_0.tabPage_6.Size = new Size(452, 192);
			form1_0.tabPage_6.TabIndex = 7;
			form1_0.tabPage_6.Text = "mobi选项";
			form1_0.tabPage_6.UseVisualStyleBackColor = true;
			form1_0.checkBox_13.AutoSize = true;
			form1_0.checkBox_13.Location = new Point(258, 24);
			form1_0.checkBox_13.Name = "checkBox_MobiForceEn";
			form1_0.checkBox_13.Size = new Size(98, 17);
			form1_0.checkBox_13.TabIndex = 31;
			form1_0.checkBox_13.Text = "兼容英文辞典";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_13, "设定mobi语言为英文以使用辞典\r\n此选项对epub/txt转mobi都有效\r\n从txt生成mobi也可在\"书籍信息\"页面设置语言\r\n但是本选项具有更高的优先级");
			form1_0.checkBox_13.UseVisualStyleBackColor = true;
			form1_0.checkBox_12.AutoSize = true;
			form1_0.checkBox_12.Location = new Point(148, 24);
			form1_0.checkBox_12.Name = "checkBox_MobiPeriodical";
			form1_0.checkBox_12.Size = new Size(74, 17);
			form1_0.checkBox_12.TabIndex = 30;
			form1_0.checkBox_12.Text = "期刊格式";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_12, "生成类似杂志/报纸的MOBI期刊格式\r\n此格式目录不能超过二级");
			form1_0.checkBox_12.UseVisualStyleBackColor = true;
			form1_0.textBox_14.CharacterCasing = CharacterCasing.Upper;
			form1_0.textBox_14.ImeMode = ImeMode.Disable;
			form1_0.textBox_14.Location = new Point(307, 59);
			form1_0.textBox_14.MaxLength = 10;
			form1_0.textBox_14.Name = "textBox_MobiASIN";
			form1_0.textBox_14.Size = new Size(88, 20);
			form1_0.textBox_14.TabIndex = 5;
			form1_0.radioButton_11.AutoSize = true;
			form1_0.radioButton_11.Checked = true;
			form1_0.radioButton_11.Location = new Point(203, 61);
			form1_0.radioButton_11.Name = "radioButton_ASINRandom";
			form1_0.radioButton_11.Size = new Size(49, 17);
			form1_0.radioButton_11.TabIndex = 3;
			form1_0.radioButton_11.TabStop = true;
			form1_0.radioButton_11.Text = "随机";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_11, "程序随机分配一个ASIN号码");
			form1_0.radioButton_11.UseVisualStyleBackColor = true;
			form1_0.radioButton_11.CheckedChanged += form1_0.method_64;
			form1_0.label_28.AutoSize = true;
			form1_0.label_28.Location = new Point(148, 63);
			form1_0.label_28.Name = "label33";
			form1_0.label_28.Size = new Size(56, 13);
			form1_0.label_28.TabIndex = 29;
			form1_0.label_28.Text = "ASIN设置";
			form1_0.toolTip_0.SetToolTip(form1_0.label_28, "设置ASIN以实现阅读进度同步功能");
			form1_0.textBox_13.Location = new Point(207, 127);
			form1_0.textBox_13.Name = "textBox_KindlegenOption";
			form1_0.textBox_13.Size = new Size(188, 20);
			form1_0.textBox_13.TabIndex = 8;
			form1_0.textBox_13.Text = "-dont_append_source";
			form1_0.comboBox_17.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_17.FormattingEnabled = true;
			form1_0.comboBox_17.Items.AddRange(new object[] { "c0", "c1", "c2" });
			form1_0.comboBox_17.Location = new Point(79, 127);
			form1_0.comboBox_17.Name = "comboBox_KindlegenCompress";
			form1_0.comboBox_17.Size = new Size(36, 21);
			form1_0.comboBox_17.TabIndex = 7;
			form1_0.label_26.AutoSize = true;
			form1_0.label_26.Location = new Point(21, 131);
			form1_0.label_26.Name = "label31";
			form1_0.label_26.Size = new Size(55, 13);
			form1_0.label_26.TabIndex = 25;
			form1_0.label_26.Text = "压缩方式";
			form1_0.toolTip_0.SetToolTip(form1_0.label_26, "MOBI文件压缩比例\r\nc0：不压缩\r\nc1：标准压缩（默认）\r\nc2：高压缩（慢，慎用）");
			form1_0.comboBox_16.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_16.FormattingEnabled = true;
			form1_0.comboBox_16.Location = new Point(105, 94);
			form1_0.comboBox_16.Name = "comboBox_KindlegenExe";
			form1_0.comboBox_16.Size = new Size(119, 21);
			form1_0.comboBox_16.TabIndex = 6;
			form1_0.comboBox_16.DropDown += form1_0.method_74;
			form1_0.label_25.AutoSize = true;
			form1_0.label_25.Location = new Point(21, 98);
			form1_0.label_25.Name = "label27";
			form1_0.label_25.Size = new Size(78, 13);
			form1_0.label_25.TabIndex = 23;
			form1_0.label_25.Text = "Kindlegen版本";
			form1_0.toolTip_0.SetToolTip(form1_0.label_25, "切换不同版本的Kindlegen\r\n文件位于bin子目录下，以kindlegen开头，\r\n接自定义后缀以区分版本\r\n比如kindlegen_v1.2.exe和kindlegen_v2.9.exe\r\n也可以只放一个单独的kindlegen.exe");
			form1_0.checkBox_10.AutoSize = true;
			form1_0.checkBox_10.Location = new Point(21, 61);
			form1_0.checkBox_10.Name = "checkBox_MobiSync";
			form1_0.checkBox_10.Size = new Size(98, 17);
			form1_0.checkBox_10.TabIndex = 2;
			form1_0.checkBox_10.Text = "阅读进度同步";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_10, "生成的MOBI文件能在不同Kindle间同步阅读进度\r\n");
			form1_0.checkBox_10.UseVisualStyleBackColor = true;
			form1_0.checkBox_10.CheckedChanged += form1_0.method_65;
			form1_0.checkBox_11.AutoSize = true;
			form1_0.checkBox_11.Location = new Point(21, 24);
			form1_0.checkBox_11.Name = "checkBox_MobiStrip";
			form1_0.checkBox_11.Size = new Size(96, 17);
			form1_0.checkBox_11.TabIndex = 1;
			form1_0.checkBox_11.Text = "精简mobi文件";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_11, "去除MOBI中冗余内容以减小文件尺寸\r\n推荐和Kindlegen v1.x配合使用\r\n如果生成MOBI失败或结果不正常请关闭此选项");
			form1_0.checkBox_11.UseVisualStyleBackColor = true;
			form1_0.label_27.AutoSize = true;
			form1_0.label_27.Location = new Point(150, 131);
			form1_0.label_27.Name = "label32";
			form1_0.label_27.Size = new Size(55, 13);
			form1_0.label_27.TabIndex = 27;
			form1_0.label_27.Text = "附加参数";
			form1_0.toolTip_0.SetToolTip(form1_0.label_27, "kindlegen的附加参数，请参考kindlegen帮助\r\n默认为空");
			form1_0.radioButton_10.AutoSize = true;
			form1_0.radioButton_10.Location = new Point(256, 61);
			form1_0.radioButton_10.Name = "radioButton_ASINFix";
			form1_0.radioButton_10.Size = new Size(49, 17);
			form1_0.radioButton_10.TabIndex = 4;
			form1_0.radioButton_10.Text = "指定";
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_10, "使用指定的ASIN号码\r\nASIN为以B00开头的10位字母+数字\r\n或者10位纯数字（ISBN-10）\r\n如果不合规格将使用随机ASIN\r\n");
			form1_0.radioButton_10.UseVisualStyleBackColor = true;
			form1_0.tabPage_5.Controls.Add(form1_0.checkBox_14);
			form1_0.tabPage_5.Controls.Add(form1_0.comboBox_19);
			form1_0.tabPage_5.Controls.Add(form1_0.comboBox_15);
			form1_0.tabPage_5.Controls.Add(form1_0.checkBox_9);
			form1_0.tabPage_5.Controls.Add(form1_0.button_9);
			form1_0.tabPage_5.Controls.Add(form1_0.checkBox_8);
			form1_0.tabPage_5.Controls.Add(form1_0.checkBox_7);
			form1_0.tabPage_5.Controls.Add(form1_0.textBox_11);
			form1_0.tabPage_5.Controls.Add(form1_0.textBox_7);
			form1_0.tabPage_5.Controls.Add(form1_0.label_19);
			form1_0.tabPage_5.Controls.Add(form1_0.textBox_8);
			form1_0.tabPage_5.Controls.Add(form1_0.label_20);
			form1_0.tabPage_5.Controls.Add(form1_0.textBox_9);
			form1_0.tabPage_5.Controls.Add(form1_0.textBox_10);
			form1_0.tabPage_5.Controls.Add(form1_0.checkBox_6);
			form1_0.tabPage_5.Controls.Add(form1_0.label_24);
			form1_0.tabPage_5.Controls.Add(form1_0.label_18);
			form1_0.tabPage_5.Controls.Add(form1_0.label_21);
			form1_0.tabPage_5.Controls.Add(form1_0.label_22);
			form1_0.tabPage_5.Controls.Add(form1_0.label_33);
			form1_0.tabPage_5.Location = new Point(4, 22);
			form1_0.tabPage_5.Name = "tabPageAdvanced";
			form1_0.tabPage_5.Padding = new Padding(3);
			form1_0.tabPage_5.Size = new Size(452, 192);
			form1_0.tabPage_5.TabIndex = 6;
			form1_0.tabPage_5.Text = " 高级 ";
			form1_0.tabPage_5.UseVisualStyleBackColor = true;
			form1_0.checkBox_14.AutoSize = true;
			form1_0.checkBox_14.Location = new Point(328, 67);
			form1_0.checkBox_14.Name = "checkBox_OutputToSrc";
			form1_0.checkBox_14.Size = new Size(98, 17);
			form1_0.checkBox_14.TabIndex = 22;
			form1_0.checkBox_14.Text = "输出到源目录";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_14, "当使用此选项时，\r\n输出目录默认为源文件所在目录");
			form1_0.checkBox_14.UseVisualStyleBackColor = true;
			form1_0.comboBox_19.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_19.FormattingEnabled = true;
			form1_0.comboBox_19.Items.AddRange(new object[] { "略过", "创建", "子目录" });
			form1_0.comboBox_19.Location = new Point(235, 31);
			form1_0.comboBox_19.Name = "comboBox_EmptyChapterStyle";
			form1_0.comboBox_19.Size = new Size(60, 21);
			form1_0.comboBox_19.TabIndex = 20;
			form1_0.comboBox_15.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_15.FormattingEnabled = true;
			form1_0.comboBox_15.Items.AddRange(new object[] { "EPUB", "MOBI", "AZW3" });
			form1_0.comboBox_15.Location = new Point(235, 64);
			form1_0.comboBox_15.Name = "comboBox_OutputFormat";
			form1_0.comboBox_15.Size = new Size(60, 21);
			form1_0.comboBox_15.TabIndex = 5;
			form1_0.checkBox_9.AutoSize = true;
			form1_0.checkBox_9.Location = new Point(328, 33);
			form1_0.checkBox_9.Name = "checkBox_TOCSpace";
			form1_0.checkBox_9.Size = new Size(98, 17);
			form1_0.checkBox_9.TabIndex = 1;
			form1_0.checkBox_9.Text = "层级目录缩进";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_9, "对不支持多级目录折叠效果的阅读器\r\n使用空格为导航栏目录增加层级缩进效果\r\n当输出MOBI时此功能始终开启");
			form1_0.checkBox_9.UseVisualStyleBackColor = true;
			form1_0.button_9.Location = new Point(391, 150);
			form1_0.button_9.Name = "button_SelectTempDir";
			form1_0.button_9.Size = new Size(24, 21);
			form1_0.button_9.TabIndex = 11;
			form1_0.button_9.Text = "...";
			form1_0.toolTip_0.SetToolTip(form1_0.button_9, "浏览选择临时文件目录");
			form1_0.button_9.UseVisualStyleBackColor = true;
			form1_0.button_9.Click += form1_0.method_62;
			form1_0.checkBox_8.AutoSize = true;
			form1_0.checkBox_8.Location = new Point(18, 152);
			form1_0.checkBox_8.Name = "checkBox_EnableTempDir";
			form1_0.checkBox_8.Size = new Size(98, 17);
			form1_0.checkBox_8.TabIndex = 9;
			form1_0.checkBox_8.Text = "临时文件目录";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_8, "自定义临时文件目录位置\r\n（推荐）不勾选则使用EasyPub目录下的temp子目录\r\n请勿设为硬盘根目录或其它重要目录！！");
			form1_0.checkBox_8.UseVisualStyleBackColor = true;
			form1_0.checkBox_8.CheckedChanged += form1_0.method_50;
			form1_0.checkBox_7.AutoSize = true;
			form1_0.checkBox_7.Location = new Point(18, 66);
			form1_0.checkBox_7.Name = "checkBox_EnableHTMLRawTag";
			form1_0.checkBox_7.Size = new Size(104, 17);
			form1_0.checkBox_7.TabIndex = 3;
			form1_0.checkBox_7.Text = "HTML源码标记";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_7, "要使用插图功能请打开此选项\r\n直接在ePub内插入非转义的HTML代码\r\nHTML源码标记不能为空，必须在行首\r\n例如：使用默认标记##，源txt中包含\r\n##<img src=\"images/hello.gif\" alt=\"hello.gif\" />\r\n将输出\r\n<img src=\"images/hello.gif\" alt=\"hello.gif\" />\r\n实现插图功能");
			form1_0.checkBox_7.UseVisualStyleBackColor = true;
			form1_0.checkBox_7.CheckedChanged += form1_0.method_49;
			form1_0.textBox_11.Location = new Point(127, 64);
			form1_0.textBox_11.Name = "textBox_HTMLRawTag";
			form1_0.textBox_11.Size = new Size(33, 20);
			form1_0.textBox_11.TabIndex = 4;
			form1_0.textBox_11.Text = "##";
			form1_0.textBox_11.Leave += form1_0.method_51;
			form1_0.textBox_7.Location = new Point(127, 94);
			form1_0.textBox_7.Name = "textBox_FlowSize";
			form1_0.textBox_7.Size = new Size(33, 20);
			form1_0.textBox_7.TabIndex = 6;
			form1_0.textBox_7.KeyPress += form1_0.method_48;
			form1_0.label_19.AutoSize = true;
			form1_0.label_19.Location = new Point(18, 98);
			form1_0.label_19.Name = "label_HTMLFlowSize";
			form1_0.label_19.Size = new Size(109, 13);
			form1_0.label_19.TabIndex = 7;
			form1_0.label_19.Text = "HTML文件大小上限";
			form1_0.toolTip_0.SetToolTip(form1_0.label_19, "单个html文件大小\r\n太小会造成不必要的分页，太大在手持设备上会有兼容性问题 \r\n建议不超过300KB。默认200KB，最低20KB\r\n此数值与逻辑章节大小（分段方式）无关");
			form1_0.textBox_8.Location = new Point(127, 150);
			form1_0.textBox_8.Name = "textBox_TempDir";
			form1_0.textBox_8.Size = new Size(263, 20);
			form1_0.textBox_8.TabIndex = 10;
			form1_0.toolTip_0.SetToolTip(form1_0.textBox_8, "输入临时文件目录");
			form1_0.label_20.AutoSize = true;
			form1_0.label_20.Location = new Point(18, 126);
			form1_0.label_20.Name = "label_ScreenSize";
			form1_0.label_20.Size = new Size(108, 13);
			form1_0.label_20.TabIndex = 4;
			form1_0.label_20.Text = "屏幕尺寸（宽x高）";
			form1_0.toolTip_0.SetToolTip(form1_0.label_20, "阅读器的分辨率。保证封面图片可以居中显示\r\n虽然大多数阅读器是600x800，但界面会占据一部分空间 \r\n所以可用尺寸会小于600x800。预设为 540x720");
			form1_0.textBox_9.Location = new Point(180, 122);
			form1_0.textBox_9.Name = "textBox_ScreenHeight";
			form1_0.textBox_9.Size = new Size(33, 20);
			form1_0.textBox_9.TabIndex = 8;
			form1_0.textBox_9.KeyPress += form1_0.method_48;
			form1_0.textBox_10.Location = new Point(127, 122);
			form1_0.textBox_10.Name = "textBox_ScreenWidth";
			form1_0.textBox_10.Size = new Size(33, 20);
			form1_0.textBox_10.TabIndex = 7;
			form1_0.textBox_10.KeyPress += form1_0.method_48;
			form1_0.checkBox_6.AutoSize = true;
			form1_0.checkBox_6.Location = new Point(18, 34);
			form1_0.checkBox_6.Name = "checkBox_SilentMode";
			form1_0.checkBox_6.Size = new Size(74, 17);
			form1_0.checkBox_6.TabIndex = 0;
			form1_0.checkBox_6.Text = "静默模式";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_6, "完成后不弹出对话框\r\n信息仅在底部状态栏显示\r\n\r\n*出错信息仍会弹出");
			form1_0.checkBox_6.UseVisualStyleBackColor = true;
			form1_0.label_24.AutoSize = true;
			form1_0.label_24.Location = new Point(180, 68);
			form1_0.label_24.Name = "label26";
			form1_0.label_24.Size = new Size(55, 13);
			form1_0.label_24.TabIndex = 19;
			form1_0.label_24.Text = "默认后缀";
			form1_0.toolTip_0.SetToolTip(form1_0.label_24, "默认输出文件名，可选EPUB或MOBI、AZW3\r\n要输出MOBI格式请下载kindlegen\r\n到程序安装目录bin子目录\r\n");
			form1_0.label_18.AutoSize = true;
			form1_0.label_18.Location = new Point(161, 98);
			form1_0.label_18.Name = "label28";
			form1_0.label_18.Size = new Size(21, 13);
			form1_0.label_18.TabIndex = 9;
			form1_0.label_18.Text = "KB";
			form1_0.label_21.AutoSize = true;
			form1_0.label_21.Location = new Point(163, 126);
			form1_0.label_21.Name = "label24";
			form1_0.label_21.Size = new Size(14, 13);
			form1_0.label_21.TabIndex = 3;
			form1_0.label_21.Text = "X";
			form1_0.label_22.AutoSize = true;
			form1_0.label_22.Location = new Point(216, 125);
			form1_0.label_22.Name = "label30";
			form1_0.label_22.Size = new Size(18, 13);
			form1_0.label_22.TabIndex = 11;
			form1_0.label_22.Text = "px";
			form1_0.label_33.AutoSize = true;
			form1_0.label_33.Location = new Point(180, 34);
			form1_0.label_33.Name = "label37";
			form1_0.label_33.Size = new Size(43, 13);
			form1_0.label_33.TabIndex = 21;
			form1_0.label_33.Text = "空章节";
			form1_0.toolTip_0.SetToolTip(form1_0.label_33, "对于只有标题而无内容的章节\r\n略过：不生成文件\r\n\u3000\u3000\u3000目录连接指向下一个不为空的章节\r\n创建：不做特殊处理（默认）\r\n\u3000\u3000\u3000生成的章节内仅含标题\r\n子目录：生成下级子目录列表\r\n\r\n*是否为空按照去空行后的结果判断\r\n不受“版式->去除空行”设置的影响");
			form1_0.linkLabel_0.AutoSize = true;
			form1_0.linkLabel_0.Location = new Point(417, 377);
			form1_0.linkLabel_0.Name = "linkLabel_HomePage";
			form1_0.linkLabel_0.Size = new Size(55, 13);
			form1_0.linkLabel_0.TabIndex = 100;
			form1_0.linkLabel_0.TabStop = true;
			form1_0.linkLabel_0.Text = "访问主页";
			form1_0.toolTip_0.SetToolTip(form1_0.linkLabel_0, "Hi-PDA论坛软件发布帖\r\n检查更新，获取最新版本\r\n欢迎报告问题和反馈意见");
			form1_0.linkLabel_0.LinkClicked += form1_0.method_60;
			form1_0.label_6.AutoSize = true;
			form1_0.label_6.Location = new Point(12, 96);
			form1_0.label_6.Name = "label_OutputFileName";
			form1_0.label_6.Size = new Size(31, 13);
			form1_0.label_6.TabIndex = 14;
			form1_0.label_6.Text = "输出";
			form1_0.toolTip_0.SetToolTip(form1_0.label_6, "输出文件名\r\n后缀为.epub则输出EPUB格式\r\n为.mobi则输出MOBI格式\r\n背景为红色表示文件存在，已有文件将被覆盖\r\n**双击此标签直接定位到输出文件");
			form1_0.label_6.DoubleClick += form1_0.method_69;
			form1_0.button_2.Location = new Point(336, 92);
			form1_0.button_2.Name = "button_SelectEpubFile";
			form1_0.button_2.Size = new Size(24, 21);
			form1_0.button_2.TabIndex = 5;
			form1_0.button_2.Text = "...";
			form1_0.toolTip_0.SetToolTip(form1_0.button_2, "选择输出文件的位置");
			form1_0.button_2.UseVisualStyleBackColor = true;
			form1_0.button_2.Click += form1_0.method_5;
			form1_0.button_4.Location = new Point(49, 123);
			form1_0.button_4.Name = "button_LaunchEditor";
			form1_0.button_4.Size = new Size(90, 26);
			form1_0.button_4.TabIndex = 6;
			form1_0.button_4.Text = "编辑TXT文件";
			form1_0.toolTip_0.SetToolTip(form1_0.button_4, "使用外置编辑器编辑txt源文件\r\n默认notepad");
			form1_0.button_4.UseVisualStyleBackColor = true;
			form1_0.button_4.Click += form1_0.method_29;
			form1_0.button_5.Location = new Point(266, 123);
			form1_0.button_5.Name = "button_StartConvert";
			form1_0.button_5.Size = new Size(94, 26);
			form1_0.button_5.TabIndex = 8;
			form1_0.button_5.Text = "开始转换";
			form1_0.toolTip_0.SetToolTip(form1_0.button_5, "Ready? Go!");
			form1_0.button_5.UseVisualStyleBackColor = true;
			form1_0.button_5.Click += form1_0.method_12;
			form1_0.button_6.BackgroundImageLayout = ImageLayout.None;
			form1_0.button_6.Location = new Point(336, 12);
			form1_0.button_6.Name = "button_SelectTextFile";
			form1_0.button_6.Size = new Size(24, 21);
			form1_0.button_6.TabIndex = 1;
			form1_0.button_6.Text = "...";
			form1_0.toolTip_0.SetToolTip(form1_0.button_6, "选择待转换的文件\r\n亦可直接拖放文件到黄色文本框");
			form1_0.button_6.UseVisualStyleBackColor = true;
			form1_0.button_6.Click += form1_0.method_4;
			form1_0.button_7.Location = new Point(139, 123);
			form1_0.button_7.Name = "button_SelectEditor";
			form1_0.button_7.Size = new Size(24, 26);
			form1_0.button_7.TabIndex = 7;
			form1_0.button_7.Text = "...";
			form1_0.toolTip_0.SetToolTip(form1_0.button_7, "选择外置txt编辑器");
			form1_0.button_7.UseVisualStyleBackColor = true;
			form1_0.button_7.Click += form1_0.method_9;
			form1_0.contextMenuStrip_0.Items.AddRange(new ToolStripItem[] { form1_0.toolStripMenuItem_1, form1_0.toolStripMenuItem_0 });
			form1_0.contextMenuStrip_0.Name = "contextMenuStrip";
			form1_0.contextMenuStrip_0.Size = new Size(153, 48);
			form1_0.toolStripMenuItem_1.Name = "ToolStripMenuItem_GenerateCover";
			form1_0.toolStripMenuItem_1.Size = new Size(152, 22);
			form1_0.toolStripMenuItem_1.Text = "生成图片封面";
			form1_0.toolStripMenuItem_1.Click += form1_0.method_63;
			form1_0.toolStripMenuItem_0.Name = "clearToolStripMenuItem";
			form1_0.toolStripMenuItem_0.Size = new Size(152, 22);
			form1_0.toolStripMenuItem_0.Text = "清除封面";
			form1_0.toolStripMenuItem_0.Click += form1_0.method_27;
			form1_0.openFileDialog_3.FileName = "openFileDialog1";
			form1_0.openFileDialog_4.FileName = "openFileDialog1";
			form1_0.toolTip_0.AutoPopDelay = 20000;
			form1_0.toolTip_0.InitialDelay = 800;
			form1_0.toolTip_0.OwnerDraw = true;
			form1_0.toolTip_0.ReshowDelay = 300;
			form1_0.toolTip_0.Draw += form1_0.method_61;
			form1_0.checkBox_3.AutoSize = true;
			form1_0.checkBox_3.Location = new Point(376, 132);
			form1_0.checkBox_3.Name = "checkBox_forcetextcover";
			form1_0.checkBox_3.Size = new Size(74, 17);
			form1_0.checkBox_3.TabIndex = 25;
			form1_0.checkBox_3.TabStop = false;
			form1_0.checkBox_3.Text = "文字封面";
			form1_0.toolTip_0.SetToolTip(form1_0.checkBox_3, "没有图片时使用书名和作者名生成文本封面 \r\n此选项对MOBI无效\r\nMOBI用户请从书名生成封面或者指定图片");
			form1_0.checkBox_3.UseVisualStyleBackColor = true;
			form1_0.pictureBox_0.BorderStyle = BorderStyle.FixedSingle;
			form1_0.pictureBox_0.Image = Class34.smethod_16();
			form1_0.pictureBox_0.InitialImage = Class34.smethod_16();
			form1_0.pictureBox_0.Location = new Point(376, 12);
			form1_0.pictureBox_0.Name = "pictureBox_Cover";
			form1_0.pictureBox_0.Size = new Size(96, 113);
			form1_0.pictureBox_0.SizeMode = PictureBoxSizeMode.StretchImage;
			form1_0.pictureBox_0.TabIndex = 5;
			form1_0.pictureBox_0.TabStop = false;
			form1_0.toolTip_0.SetToolTip(form1_0.pictureBox_0, "封面图片预览。拖放或者双击浏览设置封面图\r\n\r\n右键菜单选项\r\n生成封面图片：从书名和作者名自动生成封面图\r\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000可换字体，将ttf改名并放到fonts/title.ttf\r\n清除图片：清除封面图片");
			form1_0.pictureBox_0.DragDrop += form1_0.method_7;
			form1_0.pictureBox_0.DragEnter += form1_0.method_6;
			form1_0.pictureBox_0.DoubleClick += form1_0.method_8;
			form1_0.pictureBox_0.MouseClick += form1_0.method_26;
			form1_0.textBox_12.BackColor = SystemColors.Info;
			form1_0.textBox_12.BorderStyle = BorderStyle.None;
			form1_0.textBox_12.Font = new Font("Microsoft Sans Serif", 8f);
			form1_0.textBox_12.ForeColor = global::System.Drawing.Color.Black;
			form1_0.textBox_12.Location = new Point(12, 377);
			form1_0.textBox_12.Name = "textBox_Status";
			form1_0.textBox_12.Size = new Size(399, 13);
			form1_0.textBox_12.TabIndex = 26;
			form1_0.textBox_12.TabStop = false;
			form1_0.comboBox_27.FormattingEnabled = true;
			form1_0.comboBox_27.Location = new Point(49, 92);
			form1_0.comboBox_27.Name = "comboBox_OutputFileName";
			form1_0.comboBox_27.Size = new Size(285, 21);
			form1_0.comboBox_27.TabIndex = 101;
			form1_0.comboBox_27.DropDown += form1_0.method_74;
			form1_0.comboBox_27.TextChanged += form1_0.method_68;
			form1_0.openFileDialog_5.Filter = "EasyPub Chapter Info Files|*.epsav";
			form1_0.openFileDialog_5.Title = "Select File...";
			form1_0.label_42.AutoSize = true;
			form1_0.label_42.Location = new Point(250, 98);
			form1_0.label_42.Name = "label1";
			form1_0.label_42.Size = new Size(55, 13);
			form1_0.label_42.TabIndex = 32;
			form1_0.label_42.Text = "输出格式";
			form1_0.toolTip_0.SetToolTip(form1_0.label_42, "默认:  复合格式，mobi7+kf8，兼容性最好，尺寸最大\r\nMobi7: 单一mobi7，也可以用kindlegen 1.x直接生成\r\nKF8:   单一KF8，必须使用kindlegen 2.x");
			form1_0.comboBox_28.DropDownStyle = ComboBoxStyle.DropDownList;
			form1_0.comboBox_28.FormattingEnabled = true;
			form1_0.comboBox_28.Items.AddRange(new object[] { "默认", "Mobi7", "KF8" });
			form1_0.comboBox_28.Location = new Point(307, 94);
			form1_0.comboBox_28.Name = "comboBox_MobiFormat";
			form1_0.comboBox_28.Size = new Size(88, 21);
			form1_0.comboBox_28.TabIndex = 33;
			form1_0.AutoScaleDimensions = new SizeF(6f, 13f);
			form1_0.AutoScaleMode = AutoScaleMode.Font;
			form1_0.ClientSize = new Size(483, 399);
			form1_0.Controls.Add(form1_0.comboBox_27);
			form1_0.Controls.Add(form1_0.textBox_12);
			form1_0.Controls.Add(form1_0.linkLabel_0);
			form1_0.Controls.Add(form1_0.button_7);
			form1_0.Controls.Add(form1_0.checkBox_3);
			form1_0.Controls.Add(form1_0.button_6);
			form1_0.Controls.Add(form1_0.textBox_1);
			form1_0.Controls.Add(form1_0.button_4);
			form1_0.Controls.Add(form1_0.button_2);
			form1_0.Controls.Add(form1_0.label_6);
			form1_0.Controls.Add(form1_0.button_5);
			form1_0.Controls.Add(form1_0.label_1);
			form1_0.Controls.Add(form1_0.tabControl_0);
			form1_0.Controls.Add(form1_0.label_0);
			form1_0.Controls.Add(form1_0.textBox_2);
			form1_0.Controls.Add(form1_0.pictureBox_0);
			form1_0.Controls.Add(form1_0.textBox_0);
			form1_0.Controls.Add(form1_0.label_2);
			form1_0.FormBorderStyle = FormBorderStyle.FixedDialog;
			form1_0.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			form1_0.MaximizeBox = false;
			form1_0.Name = "Form1";
			form1_0.Text = "EasyPub";
			form1_0.FormClosing += form1_0.method_39;
			form1_0.Load += form1_0.method_38;
			form1_0.tabControl_0.ResumeLayout(false);
			form1_0.tabPage_3.ResumeLayout(false);
			form1_0.tabPage_3.PerformLayout();
			((ISupportInitialize)form1_0.numericUpDown_0).EndInit();
			form1_0.tabPage_0.ResumeLayout(false);
			form1_0.tabPage_0.PerformLayout();
			((ISupportInitialize)form1_0.numericUpDown_1).EndInit();
			form1_0.tabPage_1.ResumeLayout(false);
			form1_0.tabPage_1.PerformLayout();
			form1_0.tabPage_7.ResumeLayout(false);
			form1_0.tabPage_7.PerformLayout();
			form1_0.tabPage_2.ResumeLayout(false);
			form1_0.tabPage_2.PerformLayout();
			form1_0.tabPage_4.ResumeLayout(false);
			form1_0.tabPage_4.PerformLayout();
			((ISupportInitialize)form1_0.pictureBox_1).EndInit();
			form1_0.tabPage_6.ResumeLayout(false);
			form1_0.tabPage_6.PerformLayout();
			form1_0.tabPage_5.ResumeLayout(false);
			form1_0.tabPage_5.PerformLayout();
			form1_0.contextMenuStrip_0.ResumeLayout(false);
			((ISupportInitialize)form1_0.pictureBox_0).EndInit();
			form1_0.ResumeLayout(false);
			form1_0.PerformLayout();
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0003DCDC File Offset: 0x0003BEDC
		static byte[] smethod_142(uint uint_0, uint uint_1, byte[] byte_0)
		{
			int num = (int)Class34.smethod_83(byte_0, 20);
			int num2 = (int)Class34.smethod_83(byte_0, 128);
			byte[] array = Encoding.ASCII.GetBytes("NONE");
			byte[] array2 = byte_0;
			try
			{
				if ((num2 & 64) != 0)
				{
					array = Class34.smethod_23(byte_0.Length, byte_0, 16 + num);
					if (array.Length >= 4 && Encoding.ASCII.GetString(array, 0, 4) == "EXTH")
					{
						int num3 = (int)Class34.smethod_83(array, 8);
						int num4 = 12;
						int num5 = 0;
						while ((long)num5 < (long)((ulong)num3))
						{
							int num6 = (int)Class34.smethod_83(array, num4);
							int num7 = (int)Class34.smethod_83(array, num4 + 4);
							if (num6 == 121)
							{
								int num8 = (int)Class34.smethod_83(array, num4 + 8);
								if (uint_1 <= (uint)num8)
								{
									num8 -= (int)uint_0;
									byte[] array3 = Class34.smethod_23(16 + num + num4 + 8, byte_0, 0);
									byte[] array4 = Class34.smethod_23(byte_0.Length, byte_0, 16 + num + num4 + 8 + 4);
									byte[] array5 = Class34.smethod_79((uint)num8);
									array2 = Class34.smethod_159(array3, array5);
									array2 = Class34.smethod_159(array2, array4);
								}
							}
							num4 += num7;
							num5++;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return array2;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0003DE08 File Offset: 0x0003C008
		static void smethod_143(string string_0, string string_1, string string_2, string string_3)
		{
			string text = File.ReadAllText(string_2, Class34.smethod_179(string_2));
			text += string_0;
			string text2 = "";
			Class34.smethod_46(ref text2, text, string_1, string_3);
			Class34.smethod_125(string_1);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0003DE44 File Offset: 0x0003C044
		static void smethod_144(Form1 form1_0)
		{
			if (Directory.Exists(form1_0.string_1 + "\\META-INF"))
			{
				Class34.smethod_158(form1_0.string_1 + "\\META-INF");
			}
			if (Directory.Exists(form1_0.string_1 + "\\OEBPS"))
			{
				Class34.smethod_158(form1_0.string_1 + "\\OEBPS");
			}
			if (!Directory.Exists(form1_0.string_1))
			{
				Directory.CreateDirectory(form1_0.string_1);
			}
			Directory.CreateDirectory(form1_0.string_1 + "\\META-INF");
			Directory.CreateDirectory(form1_0.string_1 + "\\OEBPS");
			StreamWriter streamWriter = new StreamWriter(form1_0.string_1 + "\\META-INF\\container.xml");
			streamWriter.WriteLine("<?xml version=\"1.0\"?>");
			streamWriter.WriteLine("<container version=\"1.0\" xmlns=\"urn:oasis:names:tc:opendocument:xmlns:container\">");
			streamWriter.WriteLine("  <rootfiles>");
			streamWriter.WriteLine("    <rootfile full-path=\"OEBPS/content.opf\" media-type=\"application/oebps-package+xml\"/>");
			streamWriter.WriteLine("  </rootfiles>");
			streamWriter.WriteLine("</container>");
			streamWriter.Flush();
			streamWriter.Close();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000884B File Offset: 0x00006A4B
		static bool smethod_145(char char_0)
		{
			if (char_0 != '\t')
			{
				if (char_0 != ' ')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0003DF54 File Offset: 0x0003C154
		static bool smethod_146(Class27.Class28 class28_0)
		{
			int i = Class34.smethod_156(class28_0.class30_0);
			while (i >= 258)
			{
				int num;
				switch (class28_0.int_4)
				{
				case 7:
					while (((num = Class34.smethod_12(class28_0.class31_0, class28_0.class29_0)) & -256) == 0)
					{
						Class34.smethod_111(class28_0.class30_0, num);
						if (--i < 258)
						{
							return true;
						}
					}
					if (num >= 257)
					{
						class28_0.int_6 = Class27.Class28.int_0[num - 257];
						class28_0.int_5 = Class27.Class28.int_1[num - 257];
						goto IL_009C;
					}
					if (num < 0)
					{
						return false;
					}
					class28_0.class31_1 = null;
					class28_0.class31_0 = null;
					class28_0.int_4 = 2;
					return true;
				case 8:
					goto IL_009C;
				case 9:
					goto IL_00EC;
				case 10:
					break;
				default:
					continue;
				}
				IL_011F:
				if (class28_0.int_5 > 0)
				{
					class28_0.int_4 = 10;
					int num2 = Class34.smethod_21(class28_0.class29_0, class28_0.int_5);
					if (num2 < 0)
					{
						return false;
					}
					Class34.smethod_8(class28_0.class29_0, class28_0.int_5);
					class28_0.int_7 += num2;
				}
				Class34.smethod_9(class28_0.class30_0, class28_0.int_6, class28_0.int_7);
				i -= class28_0.int_6;
				class28_0.int_4 = 7;
				continue;
				IL_00EC:
				num = Class34.smethod_12(class28_0.class31_1, class28_0.class29_0);
				if (num >= 0)
				{
					class28_0.int_7 = Class27.Class28.int_2[num];
					class28_0.int_5 = Class27.Class28.int_3[num];
					goto IL_011F;
				}
				return false;
				IL_009C:
				if (class28_0.int_5 > 0)
				{
					class28_0.int_4 = 8;
					int num3 = Class34.smethod_21(class28_0.class29_0, class28_0.int_5);
					if (num3 < 0)
					{
						return false;
					}
					Class34.smethod_8(class28_0.class29_0, class28_0.int_5);
					class28_0.int_6 += num3;
				}
				class28_0.int_4 = 9;
				goto IL_00EC;
			}
			return true;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000885C File Offset: 0x00006A5C
		static bool smethod_147(Class10.Class11 class11_0)
		{
			return class11_0.bool_0;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00008864 File Offset: 0x00006A64
		static int smethod_148(string string_0)
		{
			return Encoding.UTF8.GetBytes(string_0).Length;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0003E124 File Offset: 0x0003C324
		static void smethod_149(Form2 form2_0)
		{
			form2_0.icontainer_0 = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form2));
			form2_0.button_0 = new Button();
			form2_0.label_1 = new Label();
			form2_0.label_0 = new Label();
			form2_0.textBox_0 = new TextBox();
			form2_0.button_1 = new Button();
			form2_0.textBox_1 = new TextBox();
			form2_0.toolTip_0 = new ToolTip(form2_0.icontainer_0);
			form2_0.label_2 = new Label();
			form2_0.checkBox_0 = new CheckBox();
			form2_0.linkLabel_0 = new LinkLabel();
			form2_0.label_4 = new Label();
			form2_0.linkLabel_1 = new LinkLabel();
			form2_0.numericUpDown_1 = new NumericUpDown();
			form2_0.button_2 = new Button();
			form2_0.button_3 = new Button();
			form2_0.label_3 = new Label();
			form2_0.numericUpDown_0 = new NumericUpDown();
			form2_0.panel_0 = new Panel();
			form2_0.timer_0 = new global::System.Windows.Forms.Timer(form2_0.icontainer_0);
			form2_0.toolTip_1 = new ToolTip(form2_0.icontainer_0);
			form2_0.imageList_0 = new ImageList(form2_0.icontainer_0);
			form2_0.class18_0 = new Class18();
			form2_0.columnHeader_0 = new ColumnHeader();
			form2_0.columnHeader_1 = new ColumnHeader();
			form2_0.columnHeader_2 = new ColumnHeader();
			((ISupportInitialize)form2_0.numericUpDown_1).BeginInit();
			((ISupportInitialize)form2_0.numericUpDown_0).BeginInit();
			form2_0.panel_0.SuspendLayout();
			form2_0.SuspendLayout();
			form2_0.button_0.Location = new Point(154, 397);
			form2_0.button_0.Name = "button_SaveResult";
			form2_0.button_0.Size = new Size(66, 28);
			form2_0.button_0.TabIndex = 1;
			form2_0.button_0.TabStop = false;
			form2_0.button_0.Text = "保存修改";
			form2_0.button_0.UseVisualStyleBackColor = true;
			form2_0.button_0.Click += form2_0.method_2;
			form2_0.label_1.AutoSize = true;
			form2_0.label_1.Location = new Point(11, 11);
			form2_0.label_1.Name = "label_SplitRule";
			form2_0.label_1.Size = new Size(103, 13);
			form2_0.label_1.TabIndex = 2;
			form2_0.label_1.Text = "当前正则表达式：";
			form2_0.toolTip_0.SetToolTip(form2_0.label_1, "显示当前使用的正则表达式\r\n可以复制到主窗口");
			form2_0.label_0.AutoSize = true;
			form2_0.label_0.Location = new Point(11, 86);
			form2_0.label_0.Name = "label3";
			form2_0.label_0.Size = new Size(515, 13);
			form2_0.label_0.TabIndex = 4;
			form2_0.label_0.Text = "Tab/Shift+Tab 调整层级；Enter: 删除/恢复选中目录；Delete/Shift+Delete：删除/彻底删除选中目录";
			form2_0.toolTip_0.SetToolTip(form2_0.label_0, componentResourceManager.GetString("label3.ToolTip"));
			form2_0.textBox_0.BackColor = SystemColors.Control;
			form2_0.textBox_0.BorderStyle = BorderStyle.None;
			form2_0.textBox_0.ForeColor = global::System.Drawing.Color.Maroon;
			form2_0.textBox_0.Location = new Point(16, 36);
			form2_0.textBox_0.Name = "textBox_str_reg";
			form2_0.textBox_0.ReadOnly = true;
			form2_0.textBox_0.Size = new Size(533, 13);
			form2_0.textBox_0.TabIndex = 5;
			form2_0.textBox_0.TabStop = false;
			form2_0.button_1.Location = new Point(342, 398);
			form2_0.button_1.Name = "button_Cancel";
			form2_0.button_1.Size = new Size(66, 28);
			form2_0.button_1.TabIndex = 6;
			form2_0.button_1.TabStop = false;
			form2_0.button_1.Text = "取消修改";
			form2_0.button_1.UseVisualStyleBackColor = true;
			form2_0.button_1.Click += form2_0.method_5;
			form2_0.textBox_1.BorderStyle = BorderStyle.FixedSingle;
			form2_0.textBox_1.Location = new Point(379, 403);
			form2_0.textBox_1.Name = "textBox_EditLv";
			form2_0.textBox_1.Size = new Size(173, 20);
			form2_0.textBox_1.TabIndex = 7;
			form2_0.textBox_1.TabStop = false;
			form2_0.textBox_1.Visible = false;
			form2_0.toolTip_0.AutoPopDelay = 20000;
			form2_0.toolTip_0.InitialDelay = 500;
			form2_0.toolTip_0.OwnerDraw = true;
			form2_0.toolTip_0.ReshowDelay = 100;
			form2_0.toolTip_0.Draw += form2_0.method_7;
			form2_0.label_2.AutoSize = true;
			form2_0.label_2.Location = new Point(11, 61);
			form2_0.label_2.Name = "label1";
			form2_0.label_2.Size = new Size(67, 13);
			form2_0.label_2.TabIndex = 8;
			form2_0.label_2.Text = "分割结果：";
			form2_0.toolTip_0.SetToolTip(form2_0.label_2, "预览当前章节分割结果");
			form2_0.checkBox_0.AutoSize = true;
			form2_0.checkBox_0.Location = new Point(11, 6);
			form2_0.checkBox_0.Name = "checkBox_AutoMark";
			form2_0.checkBox_0.Size = new Size(86, 17);
			form2_0.checkBox_0.TabIndex = 10;
			form2_0.checkBox_0.TabStop = false;
			form2_0.checkBox_0.Text = "标记不超过";
			form2_0.toolTip_0.SetToolTip(form2_0.checkBox_0, "用红色标记出过短的章节");
			form2_0.checkBox_0.UseVisualStyleBackColor = true;
			form2_0.checkBox_0.CheckedChanged += form2_0.method_8;
			form2_0.linkLabel_0.AutoSize = true;
			form2_0.linkLabel_0.LinkColor = global::System.Drawing.Color.Black;
			form2_0.linkLabel_0.Location = new Point(181, 9);
			form2_0.linkLabel_0.Name = "linkLabel_Delete";
			form2_0.linkLabel_0.Size = new Size(55, 13);
			form2_0.linkLabel_0.TabIndex = 0;
			form2_0.linkLabel_0.TabStop = true;
			form2_0.linkLabel_0.Text = "点击删除";
			form2_0.toolTip_0.SetToolTip(form2_0.linkLabel_0, "删除过短的章节\r\n*如需彻底删除，点击后再按Shift+Del");
			form2_0.linkLabel_0.LinkClicked += form2_0.method_11;
			form2_0.label_4.AutoSize = true;
			form2_0.label_4.Location = new Point(257, 9);
			form2_0.label_4.Name = "label_AddNewChapter";
			form2_0.label_4.Size = new Size(91, 13);
			form2_0.label_4.TabIndex = 12;
			form2_0.label_4.Text = "新章节，行号：";
			form2_0.toolTip_0.SetToolTip(form2_0.label_4, "手工输入行号添加新章节\r\n*行号不受“版式->去除空行”的影响");
			form2_0.linkLabel_1.AutoSize = true;
			form2_0.linkLabel_1.LinkColor = global::System.Drawing.Color.Black;
			form2_0.linkLabel_1.Location = new Point(397, 8);
			form2_0.linkLabel_1.Name = "linkLabel_AddNewChapter";
			form2_0.linkLabel_1.Size = new Size(55, 13);
			form2_0.linkLabel_1.TabIndex = 16;
			form2_0.linkLabel_1.TabStop = true;
			form2_0.linkLabel_1.Text = "点击添加";
			form2_0.toolTip_0.SetToolTip(form2_0.linkLabel_1, "添加新章节");
			form2_0.linkLabel_1.LinkClicked += form2_0.method_12;
			form2_0.numericUpDown_1.Location = new Point(340, 5);
			NumericUpDown numericUpDown_ = form2_0.numericUpDown_1;
			int[] array = new int[4];
			array[0] = 1;
			numericUpDown_.Minimum = new decimal(array);
			form2_0.numericUpDown_1.Name = "numericUpDown_AddNewChapter";
			form2_0.numericUpDown_1.Size = new Size(56, 20);
			form2_0.numericUpDown_1.TabIndex = 15;
			form2_0.numericUpDown_1.TabStop = false;
			form2_0.numericUpDown_1.TextAlign = HorizontalAlignment.Right;
			form2_0.toolTip_0.SetToolTip(form2_0.numericUpDown_1, "手工输入行号，按Enter添加");
			NumericUpDown numericUpDown_2 = form2_0.numericUpDown_1;
			int[] array2 = new int[4];
			array2[0] = 1;
			numericUpDown_2.Value = new decimal(array2);
			form2_0.numericUpDown_1.KeyPress += form2_0.method_14;
			form2_0.button_2.Location = new Point(16, 402);
			form2_0.button_2.Name = "button_Save";
			form2_0.button_2.Size = new Size(21, 19);
			form2_0.button_2.TabIndex = 18;
			form2_0.button_2.Text = "S";
			form2_0.toolTip_0.SetToolTip(form2_0.button_2, "保存当前章节设置");
			form2_0.button_2.UseVisualStyleBackColor = true;
			form2_0.button_2.Visible = false;
			form2_0.button_2.Click += form2_0.method_19;
			form2_0.button_3.Location = new Point(43, 402);
			form2_0.button_3.Name = "button_Load";
			form2_0.button_3.Size = new Size(21, 19);
			form2_0.button_3.TabIndex = 19;
			form2_0.button_3.Text = "L";
			form2_0.toolTip_0.SetToolTip(form2_0.button_3, "加载章节设置");
			form2_0.button_3.UseVisualStyleBackColor = true;
			form2_0.button_3.Visible = false;
			form2_0.label_3.AutoSize = true;
			form2_0.label_3.Location = new Point(128, 9);
			form2_0.label_3.Name = "label_AutoMark";
			form2_0.label_3.Size = new Size(55, 13);
			form2_0.label_3.TabIndex = 9;
			form2_0.label_3.Text = "行的章节";
			form2_0.numericUpDown_0.Location = new Point(90, 4);
			NumericUpDown numericUpDown_3 = form2_0.numericUpDown_0;
			int[] array3 = new int[4];
			array3[0] = 1;
			numericUpDown_3.Minimum = new decimal(array3);
			form2_0.numericUpDown_0.Name = "numericUpDown_AutoMark";
			form2_0.numericUpDown_0.Size = new Size(40, 20);
			form2_0.numericUpDown_0.TabIndex = 11;
			form2_0.numericUpDown_0.TabStop = false;
			NumericUpDown numericUpDown_4 = form2_0.numericUpDown_0;
			int[] array4 = new int[4];
			array4[0] = 10;
			numericUpDown_4.Value = new decimal(array4);
			form2_0.numericUpDown_0.ValueChanged += form2_0.method_9;
			form2_0.panel_0.Controls.Add(form2_0.linkLabel_0);
			form2_0.panel_0.Controls.Add(form2_0.numericUpDown_1);
			form2_0.panel_0.Controls.Add(form2_0.label_3);
			form2_0.panel_0.Controls.Add(form2_0.numericUpDown_0);
			form2_0.panel_0.Controls.Add(form2_0.label_4);
			form2_0.panel_0.Controls.Add(form2_0.checkBox_0);
			form2_0.panel_0.Controls.Add(form2_0.linkLabel_1);
			form2_0.panel_0.Location = new Point(94, 54);
			form2_0.panel_0.Name = "panel_ByContent";
			form2_0.panel_0.Size = new Size(453, 29);
			form2_0.panel_0.TabIndex = 17;
			form2_0.timer_0.Enabled = true;
			form2_0.timer_0.Interval = 400;
			form2_0.toolTip_1.AutomaticDelay = 700;
			form2_0.toolTip_1.AutoPopDelay = 15000;
			form2_0.toolTip_1.InitialDelay = 700;
			form2_0.toolTip_1.OwnerDraw = true;
			form2_0.toolTip_1.ReshowDelay = 700;
			form2_0.toolTip_1.Draw += form2_0.method_16;
			form2_0.imageList_0.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList.ImageStream");
			form2_0.imageList_0.Tag = "";
			form2_0.imageList_0.TransparentColor = global::System.Drawing.Color.Transparent;
			form2_0.imageList_0.Images.SetKeyName(0, "red-x-white");
			form2_0.imageList_0.Images.SetKeyName(1, "white");
			form2_0.imageList_0.Images.SetKeyName(2, "lightgreen");
			form2_0.imageList_0.Images.SetKeyName(3, "red-x-lightgreen.png");
			form2_0.class18_0.AllowColumnReorder = true;
			form2_0.class18_0.Columns.AddRange(new ColumnHeader[] { form2_0.columnHeader_0, form2_0.columnHeader_1, form2_0.columnHeader_2 });
			form2_0.class18_0.bool_1 = true;
			form2_0.class18_0.ForeColor = global::System.Drawing.Color.Black;
			form2_0.class18_0.FullRowSelect = true;
			form2_0.class18_0.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			form2_0.class18_0.HideSelection = false;
			form2_0.class18_0.Location = new Point(14, 103);
			form2_0.class18_0.bool_0 = true;
			form2_0.class18_0.Name = "lv";
			form2_0.class18_0.Size = new Size(538, 285);
			form2_0.class18_0.TabIndex = 0;
			form2_0.class18_0.TabStop = false;
			form2_0.class18_0.UseCompatibleStateImageBehavior = false;
			form2_0.class18_0.View = View.Details;
			Class18 class18_ = form2_0.class18_0;
			Delegate1 @delegate = new Delegate1(form2_0.method_6);
			Class34.smethod_95(@delegate, class18_);
			form2_0.class18_0.KeyDown += form2_0.method_4;
			form2_0.class18_0.MouseEnter += form2_0.method_17;
			form2_0.class18_0.MouseLeave += form2_0.method_18;
			form2_0.class18_0.MouseMove += form2_0.method_15;
			form2_0.columnHeader_0.Text = "序号";
			form2_0.columnHeader_0.Width = 86;
			form2_0.columnHeader_1.Text = "章节名称（双击编辑）";
			form2_0.columnHeader_1.Width = 370;
			form2_0.columnHeader_2.Text = "行号";
			form2_0.AutoScaleDimensions = new SizeF(6f, 13f);
			form2_0.AutoScaleMode = AutoScaleMode.Font;
			form2_0.ClientSize = new Size(564, 428);
			form2_0.Controls.Add(form2_0.button_3);
			form2_0.Controls.Add(form2_0.button_2);
			form2_0.Controls.Add(form2_0.panel_0);
			form2_0.Controls.Add(form2_0.button_1);
			form2_0.Controls.Add(form2_0.label_2);
			form2_0.Controls.Add(form2_0.textBox_1);
			form2_0.Controls.Add(form2_0.textBox_0);
			form2_0.Controls.Add(form2_0.label_0);
			form2_0.Controls.Add(form2_0.label_1);
			form2_0.Controls.Add(form2_0.button_0);
			form2_0.Controls.Add(form2_0.class18_0);
			form2_0.FormBorderStyle = FormBorderStyle.FixedDialog;
			form2_0.MaximizeBox = false;
			form2_0.Name = "Form2";
			form2_0.Text = "章节编辑   - ";
			form2_0.FormClosing += form2_0.method_3;
			form2_0.Load += form2_0.method_10;
			((ISupportInitialize)form2_0.numericUpDown_1).EndInit();
			((ISupportInitialize)form2_0.numericUpDown_0).EndInit();
			form2_0.panel_0.ResumeLayout(false);
			form2_0.panel_0.PerformLayout();
			form2_0.ResumeLayout(false);
			form2_0.PerformLayout();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0003F0F4 File Offset: 0x0003D2F4
		static byte[] smethod_150(int int_0, byte[] byte_0, int int_1)
		{
			byte[] array = new byte[0];
			Tuple<int, int> tuple = Class34.smethod_81(byte_0, int_1);
			int item = tuple.Item1;
			int item2 = tuple.Item2;
			tuple = Class34.smethod_81(byte_0, int_0);
			int item3 = tuple.Item1;
			int item4 = tuple.Item2;
			tuple = Class34.smethod_81(byte_0, 0);
			int item5 = tuple.Item1;
			int item6 = tuple.Item2;
			int num = item4 - item + 8 * (int_0 - int_1 + 1);
			short num2 = (short)((ushort)Class34.smethod_165(byte_0, Class5.Class6.int_1, "H"));
			array = array.smethod_0(byte_0.smethod_6(0, Class5.Class6.int_0));
			int num3 = 2 * ((int)num2 - (int_0 - int_1 + 1)) + 1;
			array = array.smethod_0(((uint)num3).smethod_5());
			array = array.smethod_0(byte_0.smethod_6(Class5.Class6.int_0 + 4, Class5.Class6.int_1));
			array = array.smethod_0(((ushort)((int)num2 - (int_0 - int_1 + 1))).smethod_4());
			int num4 = item5 - 8 * (int_0 - int_1 + 1);
			for (int i = 0; i < int_1; i++)
			{
				int num5 = (int)byte_0.smethod_3(Class5.Class6.int_4 + i * 8);
				int num6 = (int)byte_0.smethod_3(Class5.Class6.int_4 + i * 8 + 4);
				num5 -= 8 * (int_0 - int_1 + 1);
				array = array.smethod_0(((uint)num5).smethod_5());
				array = array.smethod_0(((uint)num6).smethod_5());
			}
			for (int j = int_0 + 1; j < (int)num2; j++)
			{
				int num7 = (int)byte_0.smethod_3(Class5.Class6.int_4 + j * 8);
				int num8 = (int)byte_0.smethod_3(Class5.Class6.int_4 + j * 8 + 4);
				num7 -= num;
				num8 = 2 * (j - (int_0 - int_1 + 1));
				array = array.smethod_0(((uint)num7).smethod_5());
				array = array.smethod_0(((uint)num8).smethod_5());
			}
			int num9 = num4 - (Class5.Class6.int_4 + 8 * ((int)num2 - (int_0 - int_1 + 1)));
			if (num9 > 0)
			{
				byte[] array2 = Enumerable.Repeat<byte>(0, num9).ToArray<byte>();
				array = array.smethod_0(array2);
			}
			array = array.smethod_0(byte_0.smethod_6(item5, item));
			return array.smethod_0(byte_0.smethod_6(item4, byte_0.Length));
		}

		// Token: 0x0600068D RID: 1677
		[DllImport("user32.dll")]
		static extern int MoveWindow(IntPtr intptr_0, int int_0, int int_1, int int_2, int int_3, bool bool_0);

		// Token: 0x0600068E RID: 1678
		[DllImport("User32.dll", EntryPoint = "SendMessage")]
		static extern IntPtr SendMessage_2(HandleRef handleRef_0, int int_0, int int_1, int int_2);

		// Token: 0x0600068F RID: 1679 RVA: 0x00008873 File Offset: 0x00006A73
		static int smethod_151(Class27.Stream0 stream0_0)
		{
			return stream0_0.ReadByte() | (stream0_0.ReadByte() << 8);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0003F2F0 File Offset: 0x0003D4F0
		static void smethod_152(string string_0, Class10 class10_0)
		{
			string text = "/" + string_0;
			while (!Class34.smethod_121(class10_0))
			{
				if (Class34.smethod_88(class10_0) == '<')
				{
					bool flag;
					if (Class34.smethod_72(class10_0, ref flag) == text)
					{
						return;
					}
					if (!flag && !string_0.StartsWith("/"))
					{
						Class34.smethod_152(string_0, class10_0);
					}
				}
				else
				{
					Class34.smethod_87(class10_0);
				}
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00008884 File Offset: 0x00006A84
		static DialogResult smethod_153(IWin32Window iwin32Window_0, string string_0, string string_1)
		{
			Class8.iwin32Window_0 = iwin32Window_0;
			Class34.smethod_178();
			return MessageBox.Show(iwin32Window_0, string_0, string_1);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0003F350 File Offset: 0x0003D550
		static void smethod_154(Form2 form2_0, global::System.Drawing.Color color_0)
		{
			if (!form2_0.bool_0)
			{
				return;
			}
			if (form2_0.class18_0.Items.Count <= 0)
			{
				return;
			}
			foreach (object obj in form2_0.class18_0.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.ForeColor = global::System.Drawing.Color.Black;
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00008899 File Offset: 0x00006A99
		static string smethod_155(EventArgs1 eventArgs1_0)
		{
			return eventArgs1_0.string_0;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000088A1 File Offset: 0x00006AA1
		static int smethod_156(Class27.Class30 class30_0)
		{
			return 32768 - class30_0.int_1;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000088AF File Offset: 0x00006AAF
		static void smethod_157(Class10 class10_0)
		{
			while (Class34.smethod_145(Class34.smethod_88(class10_0)))
			{
				Class34.smethod_87(class10_0);
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0003F3D0 File Offset: 0x0003D5D0
		static void smethod_158(string string_0)
		{
			if (!Directory.Exists(string_0))
			{
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(string_0);
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				fileInfo.IsReadOnly = false;
				fileInfo.Delete();
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				Class34.smethod_158(directoryInfo2.FullName);
				directoryInfo2.Delete();
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000089C0 File Offset: 0x00006BC0
		static byte[] smethod_159(byte[] byte_0, byte[] byte_1)
		{
			byte[] array = new byte[byte_0.Length + byte_1.Length];
			Buffer.BlockCopy(byte_0, 0, array, 0, byte_0.Length);
			Buffer.BlockCopy(byte_1, 0, array, byte_0.Length, byte_1.Length);
			return array;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0003F448 File Offset: 0x0003D648
		static byte[] smethod_160(Class13 class13_0, byte[] byte_0, int int_0, int int_1)
		{
			byte[] array = new byte[int_1 - int_0];
			Buffer.BlockCopy(byte_0, int_0, array, 0, int_1 - int_0);
			return array;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0003F46C File Offset: 0x0003D66C
		static void smethod_161(Class15.Struct6 struct6_0)
		{
			for (int i = 0; i < Class15.list_0.Count; i++)
			{
				if (Class15.list_0[i].string_0 == struct6_0.string_0)
				{
					Class15.list_0[i] = struct6_0;
					return;
				}
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0003F4BC File Offset: 0x0003D6BC
		static void smethod_162(Class27.Class30 class30_0, int int_0, int int_1, int int_2)
		{
			while (int_1-- > 0)
			{
				class30_0.byte_0[class30_0.int_0++] = class30_0.byte_0[int_0++];
				class30_0.int_0 &= 32767;
				int_0 &= 32767;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0003F518 File Offset: 0x0003D718
		static void smethod_163(byte[] byte_0, Class27.Class31 class31_0)
		{
			int[] array = new int[16];
			int[] array2 = new int[16];
			foreach (int num in byte_0)
			{
				if (num > 0)
				{
					array[num]++;
				}
			}
			int num2 = 0;
			int num3 = 512;
			for (int j = 1; j <= 15; j++)
			{
				array2[j] = num2;
				num2 += array[j] << 16 - j;
				if (j >= 10)
				{
					int num4 = array2[j] & 130944;
					int num5 = num2 & 130944;
					num3 += num5 - num4 >> 16 - j;
				}
			}
			class31_0.short_0 = new short[num3];
			int num6 = 512;
			for (int k = 15; k >= 10; k--)
			{
				int num7 = num2 & 130944;
				num2 -= array[k] << 16 - k;
				int num8 = num2 & 130944;
				for (int l = num8; l < num7; l += 128)
				{
					class31_0.short_0[(int)Class34.smethod_172(l)] = (short)((-num6 << 4) | k);
					num6 += 1 << k - 9;
				}
			}
			for (int m = 0; m < byte_0.Length; m++)
			{
				int num9 = (int)byte_0[m];
				if (num9 != 0)
				{
					num2 = array2[num9];
					int num10 = (int)Class34.smethod_172(num2);
					if (num9 <= 9)
					{
						do
						{
							class31_0.short_0[num10] = (short)((m << 4) | num9);
							num10 += 1 << num9;
						}
						while (num10 < 512);
					}
					else
					{
						int num11 = (int)class31_0.short_0[num10 & 511];
						int num12 = 1 << (num11 & 15);
						num11 = -(num11 >> 4);
						do
						{
							class31_0.short_0[num11 | (num10 >> 9)] = (short)((m << 4) | num9);
							num10 += 1 << num9;
						}
						while (num10 < num12);
					}
					array2[num9] = num2 + (1 << 16 - num9);
				}
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0003F70C File Offset: 0x0003D90C
		static void smethod_164(Form1 form1_0)
		{
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_3, string.Format("外挂ttf字体显示中文\n所有设置保存在{0}\n可自行修改", form1_0.struct3_0.string_24));
			form1_0.toolTip_0.SetToolTip(form1_0.label_17, string.Format("输入额外的表达式，和“简易规则”配合使用\n下拉框中的预定义表达式保存在{0}中，可自行修改", form1_0.string_4));
			form1_0.toolTip_0.SetToolTip(form1_0.radioButton_5, string.Format("直接输入完整的正则表达式。\n下拉框中的预定义设置保存在{0}中，可自行修改", form1_0.string_4));
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000088C7 File Offset: 0x00006AC7
		static uint smethod_165(byte[] byte_0, int int_0, string string_0 = "L")
		{
			if (string_0 == "L")
			{
				return byte_0.smethod_3(int_0);
			}
			return (uint)byte_0.smethod_2(int_0);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00005233 File Offset: 0x00003433
		static void smethod_166(CancelEventArgs cancelEventArgs_0, Settings settings_0, object object_0)
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0003F784 File Offset: 0x0003D984
		static void smethod_167(Form1 form1_0)
		{
			Class34.smethod_118(form1_0, "准备图片文件", "normal");
			if (form1_0.listView_0.Items.Count > 0)
			{
				Directory.CreateDirectory(form1_0.string_1 + "\\OEBPS\\images");
				foreach (object obj in form1_0.listView_0.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					try
					{
						string text = listViewItem.Text;
						File.Copy(text, form1_0.string_1 + "\\OEBPS\\images\\" + Path.GetFileName(text), true);
					}
					catch (Exception ex)
					{
						Class34.smethod_118(form1_0, ex.Message, "warning");
					}
				}
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000088E5 File Offset: 0x00006AE5
		static void smethod_168(Class10.Class11 class11_0)
		{
			class11_0.stringBuilder_0.Length = 0;
			class11_0.stringBuilder_1.Length = 0;
			class11_0.int_0 = 0;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00008906 File Offset: 0x00006B06
		static DialogResult smethod_169(string string_0, string string_1)
		{
			Class34.smethod_178();
			return MessageBox.Show(string_0, string_1);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00008AA0 File Offset: 0x00006CA0
		static byte[] smethod_170(uint uint_0, Class13 class13_0)
		{
			byte[] bytes = BitConverter.GetBytes(uint_0);
			Array.Reverse(bytes, 0, bytes.Length);
			return bytes;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0003F864 File Offset: 0x0003DA64
		static bool smethod_171(Assembly assembly_0, Assembly assembly_1)
		{
			byte[] publicKey = assembly_0.GetName().GetPublicKey();
			byte[] publicKey2 = assembly_1.GetName().GetPublicKey();
			if (publicKey2 == null != (publicKey == null))
			{
				return false;
			}
			if (publicKey2 != null)
			{
				for (int i = 0; i < publicKey2.Length; i++)
				{
					if (publicKey2[i] != publicKey[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060006A4 RID: 1700
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		static extern IntPtr SendMessage_3(IntPtr intptr_0, int int_0, int int_1, int int_2);

		// Token: 0x060006A5 RID: 1701 RVA: 0x00008914 File Offset: 0x00006B14
		static short smethod_172(int int_0)
		{
			return (short)(((int)Class27.Class33.byte_0[int_0 & 15] << 12) | ((int)Class27.Class33.byte_0[(int_0 >> 4) & 15] << 8) | ((int)Class27.Class33.byte_0[(int_0 >> 8) & 15] << 4) | (int)Class27.Class33.byte_0[int_0 >> 12]);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0003F8B8 File Offset: 0x0003DAB8
		static byte[] smethod_173(byte[] byte_0, int int_0, byte[] byte_1)
		{
			byte[] array = new byte[0];
			int num = (int)Class34.smethod_165(byte_0, Class5.Class6.int_1, "H");
			Tuple<int, int> tuple = Class34.smethod_81(byte_0, 0);
			int item = tuple.Item1;
			int item2 = tuple.Item2;
			tuple = Class34.smethod_81(byte_0, int_0);
			int item3 = tuple.Item1;
			int item4 = tuple.Item2;
			int num2 = byte_1.Length - (item4 - item3);
			array = array.smethod_0(byte_0.smethod_6(0, Class5.Class6.int_0));
			array = array.smethod_0(((uint)(2 * num + 1)).smethod_5());
			array = array.smethod_0(byte_0.smethod_6(Class5.Class6.int_0 + 4, Class5.Class6.int_1));
			array = array.smethod_0(((ushort)num).smethod_4());
			int num3 = item;
			for (int i = 0; i < int_0; i++)
			{
				int num4 = (int)byte_0.smethod_3(Class5.Class6.int_4 + i * 8);
				int num5 = (int)byte_0.smethod_3(Class5.Class6.int_4 + i * 8 + 4);
				array = array.smethod_0(((uint)num4).smethod_5());
				array = array.smethod_0(((uint)num5).smethod_5());
			}
			array = array.smethod_0(((uint)item3).smethod_5());
			array = array.smethod_0(((uint)(2 * int_0)).smethod_5());
			int num6 = int_0 + 1;
			while ((long)num6 < (long)((ulong)num))
			{
				int num7 = (int)byte_0.smethod_3(Class5.Class6.int_4 + num6 * 8);
				int num8 = (int)byte_0.smethod_3(Class5.Class6.int_4 + num6 * 8 + 4);
				num7 += num2;
				array = array.smethod_0(((uint)num7).smethod_5());
				array = array.smethod_0(((uint)num8).smethod_5());
				num6++;
			}
			long num9 = (long)num3 - ((long)Class5.Class6.int_4 + (long)((ulong)(8 * num)));
			if (num9 > 0L)
			{
				byte[] array2 = Enumerable.Repeat<byte>(0, (int)num9).ToArray<byte>();
				array = array.smethod_0(array2);
			}
			array = array.smethod_0(byte_0.smethod_6(item, item3));
			array = array.smethod_0(byte_1);
			return array.smethod_0(byte_0.smethod_6(item4, byte_0.Length));
		}

		// Token: 0x060006A7 RID: 1703
		[DllImport("user32.dll")]
		static extern bool CheckMenuItem(IntPtr intptr_0, int int_0, int int_1);

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000894D File Offset: 0x00006B4D
		static string smethod_174(string string_0, Form1 form1_0)
		{
			return string_0.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0003FA94 File Offset: 0x0003DC94
		static Class27.Class31 smethod_175(Class27.Class32 class32_0)
		{
			byte[] array = new byte[class32_0.int_4];
			Array.Copy(class32_0.byte_1, class32_0.int_3, array, 0, class32_0.int_4);
			return new Class27.Class31(array);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0003FACC File Offset: 0x0003DCCC
		static ushort smethod_176(Class13 class13_0, byte[] byte_0, int int_0)
		{
			ushort num = BitConverter.ToUInt16(byte_0, int_0);
			return (ushort)(((int)(num & 255) << 8) | ((num & 65280) >> 8));
		}

		// Token: 0x060006AB RID: 1707
		[DllImport("USER32", CharSet = CharSet.Unicode, EntryPoint = "GetSystemMenu", ExactSpelling = true, SetLastError = true)]
		static extern IntPtr GetSystemMenu_1(IntPtr intptr_0, int int_0);

		// Token: 0x060006AC RID: 1708 RVA: 0x0003FAF8 File Offset: 0x0003DCF8
		static void smethod_177(Form1 form1_0)
		{
			FileStream fileStream = File.OpenWrite(form1_0.string_1 + "\\OEBPS\\content.opf");
			StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
			streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?>");
			streamWriter.WriteLine();
			streamWriter.WriteLine("<package version=\"2.0\" xmlns=\"http://www.idpf.org/2007/opf\" unique-identifier=\"bookid\">");
			streamWriter.WriteLine("<metadata xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:opf=\"http://www.idpf.org/2007/opf\">");
			form1_0.method_19(streamWriter);
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\cover.jpg"))
			{
				streamWriter.WriteLine("<meta name=\"cover\" content=\"cover-image\"/>");
			}
			streamWriter.WriteLine("</metadata>");
			streamWriter.WriteLine("<manifest>");
			streamWriter.WriteLine("<item id=\"ncxtoc\" href=\"toc.ncx\" media-type=\"application/x-dtbncx+xml\"/>");
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\book-toc.html"))
			{
				streamWriter.WriteLine("<item id=\"htmltoc\"  href=\"book-toc.html\" media-type=\"application/xhtml+xml\"/>");
			}
			streamWriter.WriteLine("<item id=\"css\" href=\"style.css\" media-type=\"text/css\"/>");
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\cover.jpg"))
			{
				streamWriter.WriteLine("<item id=\"cover-image\" href=\"cover.jpg\" media-type=\"image/jpeg\"/>");
			}
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\cover.html"))
			{
				streamWriter.WriteLine("<item id=\"cover\" href=\"cover.html\" media-type=\"application/xhtml+xml\"/>");
			}
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\" + form1_0.struct1_0.string_2 + ".ttf"))
			{
				streamWriter.WriteLine("<item id=\"embedded-font\" href=\"" + form1_0.struct1_0.string_2 + ".ttf\" media-type=\"application/x-truetype-font\"/>");
			}
			if (Directory.Exists(form1_0.string_1 + "\\OEBPS\\images"))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(form1_0.string_1 + "\\OEBPS\\images");
				int num = 0;
				FileInfo[] files = directoryInfo.GetFiles("*.*");
				foreach (FileInfo fileInfo in files)
				{
					string text = fileInfo.Extension.ToLower();
					if (!(text == ".jpg") && !(text == ".jpeg"))
					{
						if (text == ".gif")
						{
							streamWriter.WriteLine(string.Format("<item id=\"inspic{0}\" href=\"images/{1}\" media-type=\"image/gif\"/>", num, HttpUtility.HtmlEncode(fileInfo.Name)));
							num++;
						}
						else if (text == ".png")
						{
							streamWriter.WriteLine(string.Format("<item id=\"inspic{0}\" href=\"images/{1}\" media-type=\"image/png\"/>", num, HttpUtility.HtmlEncode(fileInfo.Name)));
							num++;
						}
					}
					else
					{
						streamWriter.WriteLine(string.Format("<item id=\"inspic{0}\" href=\"images/{1}\" media-type=\"image/jpeg\"/>", num, HttpUtility.HtmlEncode(fileInfo.Name)));
						num++;
					}
				}
			}
			for (int j = 0; j < form1_0.list_2.Count + 10; j++)
			{
				string text2 = "chapter" + j;
				if (File.Exists(form1_0.string_1 + "\\OEBPS\\" + text2 + ".html"))
				{
					streamWriter.WriteLine(string.Format("<item id=\"{0}\" href=\"{0}.html\" media-type=\"application/xhtml+xml\"/>", text2));
				}
				int k = 1;
				while (k < 9999 && File.Exists(string.Concat(new object[] { form1_0.string_1, "\\OEBPS\\", text2, "-", k, ".html" })))
				{
					streamWriter.WriteLine(string.Format("<item id=\"{0}\" href=\"{0}.html\" media-type=\"application/xhtml+xml\"/>", text2 + "-" + k));
					k++;
				}
			}
			streamWriter.WriteLine("</manifest>");
			streamWriter.WriteLine("<spine toc=\"ncxtoc\">");
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\cover.html"))
			{
				streamWriter.WriteLine("<itemref idref=\"cover\" linear=\"no\"/>");
			}
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\book-toc.html"))
			{
				streamWriter.WriteLine("<itemref idref=\"htmltoc\" linear=\"yes\"/>");
			}
			for (int j = 0; j < form1_0.list_2.Count + 10; j++)
			{
				string text3 = "chapter" + j;
				if (File.Exists(form1_0.string_1 + "\\OEBPS\\" + text3 + ".html"))
				{
					streamWriter.WriteLine(string.Format("<itemref idref=\"{0}\" linear=\"yes\"/>", text3));
				}
				int k = 1;
				while (k < 9999 && File.Exists(string.Concat(new object[] { form1_0.string_1, "\\OEBPS\\", text3, "-", k, ".html" })))
				{
					streamWriter.WriteLine(string.Format("<itemref idref=\"{0}\" linear=\"yes\"/>", text3 + "-" + k));
					k++;
				}
			}
			streamWriter.WriteLine("</spine>");
			streamWriter.WriteLine("<guide>");
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\cover.html"))
			{
				streamWriter.WriteLine("<reference href=\"cover.html\" type=\"cover\" title=\"Cover\"/>");
			}
			if (File.Exists(form1_0.string_1 + "\\OEBPS\\book-toc.html"))
			{
				streamWriter.WriteLine("<reference href=\"book-toc.html\" type=\"toc\" title=\"Table Of Contents\"/>");
			}
			for (int k = 0; k < form1_0.list_2.Count; k++)
			{
				if (File.Exists(string.Concat(new object[] { form1_0.string_1, "\\OEBPS\\chapter", k, ".html" })))
				{
					streamWriter.WriteLine(string.Format("<reference href=\"chapter{0}.html\" type=\"text\" title=\"Beginning\"/>", k));
					IL_0563:
					streamWriter.WriteLine("</guide>");
					streamWriter.WriteLine("</package>");
					streamWriter.Flush();
					streamWriter.Close();
					return;
				}
			}
			goto IL_0563;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000897D File Offset: 0x00006B7D
		static void smethod_178()
		{
			if (Class8.intptr_0 != IntPtr.Zero)
			{
				throw new NotSupportedException("multiple calls are not supported");
			}
			if (Class8.iwin32Window_0 != null)
			{
				Class8.intptr_0 = Class34.SetWindowsHookEx(12, Class8.delegate0_0, IntPtr.Zero, Class34.GetCurrentThreadId());
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0004008C File Offset: 0x0003E28C
		static Encoding smethod_179(string string_0)
		{
			Encoding encoding = Encoding.Default;
			try
			{
				using (FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read))
				{
					byte[] array = new byte[5];
					if (fileStream.Length <= 5L)
					{
						return Encoding.Default;
					}
					fileStream.Read(array, 0, 4);
					string text = BitConverter.ToString(array).Replace("-", string.Empty);
					if (text.Substring(0, 6) == "EFBBBF")
					{
						encoding = Encoding.UTF8;
					}
					else if (text.Substring(0, 4) == "FEFF")
					{
						encoding = Encoding.BigEndianUnicode;
					}
					else if (text.Substring(0, 8) == "FFFE0000")
					{
						encoding = Encoding.UTF32;
					}
					else if (text.Substring(0, 4) == "FFFE")
					{
						encoding = Encoding.Unicode;
					}
					else
					{
						fileStream.Seek(0L, SeekOrigin.Begin);
						int num = (int)fileStream.Length;
						byte[] array2 = new byte[num];
						int num2 = 0;
						while ((array2[num2] = (byte)fileStream.ReadByte()) >= 0)
						{
							num2++;
							if (num2 >= num || (array2[num2 - 1] == 10 && num2 > 300000))
							{
								break;
							}
						}
						if (Class34.smethod_80(array2, 0, num2))
						{
							encoding = Encoding.UTF8;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return encoding;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0004021C File Offset: 0x0003E41C
		static void smethod_180(Form1 form1_0)
		{
			if (form1_0.comboBox_21.Text.Trim() != "")
			{
				form1_0.string_9 = form1_0.comboBox_21.Text.Trim();
			}
			if (form1_0.string_9 != "zh")
			{
				try
				{
					CultureInfo.GetCultureInfo(form1_0.string_9);
				}
				catch (Exception)
				{
					form1_0.string_9 = "zh-CN";
				}
			}
			if (form1_0.bool_1 && form1_0.checkBox_13.Checked)
			{
				form1_0.string_9 = "en-US";
			}
			if (form1_0.radioButton_4.Checked)
			{
				form1_0.struct3_0.string_23 = "0";
			}
			else if (form1_0.radioButton_5.Checked)
			{
				form1_0.struct3_0.string_23 = "1";
			}
			else if (form1_0.radioButton_6.Checked)
			{
				form1_0.struct3_0.string_23 = "2";
			}
			else
			{
				form1_0.struct3_0.string_23 = "0";
			}
			form1_0.struct3_0.int_13 = (int)form1_0.numericUpDown_0.Value;
			form1_0.struct3_0.string_6 = form1_0.comboBox_5.Text;
			form1_0.struct3_0.int_2 = form1_0.comboBox_23.SelectedIndex;
			form1_0.struct3_0.string_7 = form1_0.comboBox_8.Text;
			form1_0.struct3_0.int_3 = form1_0.comboBox_26.SelectedIndex;
			form1_0.struct3_0.string_8 = form1_0.comboBox_7.Text;
			form1_0.struct3_0.int_4 = form1_0.comboBox_25.SelectedIndex;
			form1_0.struct3_0.string_9 = form1_0.comboBox_6.Text;
			form1_0.struct3_0.int_5 = form1_0.comboBox_24.SelectedIndex;
			form1_0.struct3_0.string_11 = form1_0.comboBox_0.Text;
			form1_0.struct3_0.string_18 = form1_0.comboBox_10.Text;
			form1_0.struct3_0.string_12 = form1_0.comboBox_9.Text;
			form1_0.struct3_0.int_6 = form1_0.comboBox_20.SelectedIndex;
			form1_0.struct3_0.string_10 = form1_0.comboBox_11.Text;
			form1_0.struct3_0.int_7 = (form1_0.checkBox_2.Checked ? 1 : 0);
			form1_0.struct3_0.int_9 = form1_0.comboBox_14.SelectedIndex;
			form1_0.struct3_0.int_10 = form1_0.comboBox_18.SelectedIndex;
			form1_0.struct3_0.string_14 = form1_0.textBox_16.Text;
			form1_0.struct3_0.string_15 = form1_0.textBox_15.Text;
			if (form1_0.radioButton_3.Checked)
			{
				form1_0.struct3_0.string_19 = "0";
			}
			else if (form1_0.radioButton_2.Checked)
			{
				form1_0.struct3_0.string_19 = "1";
			}
			else if (form1_0.radioButton_1.Checked)
			{
				form1_0.struct3_0.string_19 = "2";
			}
			else
			{
				form1_0.struct3_0.string_19 = "3";
			}
			form1_0.struct3_0.int_11 = form1_0.comboBox_1.SelectedIndex;
			form1_0.struct3_0.string_16 = form1_0.textBox_5.Text;
			form1_0.struct3_0.string_17 = form1_0.textBox_3.Text;
			if (form1_0.checkBox_0.Checked)
			{
				form1_0.struct3_0.int_12 = 1;
			}
			else
			{
				form1_0.struct3_0.int_12 = 0;
			}
			if (form1_0.radioButton_9.Checked)
			{
				form1_0.struct3_0.string_20 = "0";
			}
			else if (form1_0.radioButton_8.Checked)
			{
				form1_0.struct3_0.string_20 = "1";
			}
			else
			{
				form1_0.struct3_0.string_20 = "2";
			}
			form1_0.struct3_0.string_1 = (form1_0.checkBox_3.Checked ? "1" : "0");
			form1_0.struct3_0.string_13 = (form1_0.checkBox_4.Checked ? "1" : "0");
			form1_0.struct3_0.int_8 = (int)form1_0.numericUpDown_1.Value;
			form1_0.struct3_0.string_21 = (form1_0.checkBox_5.Checked ? "1" : "0");
			form1_0.struct3_0.int_0 = (form1_0.checkBox_1.Checked ? 1 : 0);
			form1_0.struct3_0.string_2 = form1_0.comboBox_2.Text;
			form1_0.struct3_0.int_1 = form1_0.comboBox_4.SelectedIndex;
			form1_0.struct3_0.string_3 = form1_0.comboBox_3.Text;
			form1_0.struct3_0.string_4 = form1_0.comboBox_13.Text;
			form1_0.struct3_0.string_5 = form1_0.comboBox_12.Text;
			form1_0.struct2_0.int_10 = (form1_0.checkBox_11.Checked ? 1 : 0);
			form1_0.struct2_0.int_11 = (form1_0.checkBox_12.Checked ? 1 : 0);
			form1_0.struct2_0.int_12 = (form1_0.checkBox_13.Checked ? 1 : 0);
			form1_0.struct2_0.int_13 = (form1_0.checkBox_10.Checked ? 1 : 0);
			form1_0.struct2_0.int_14 = (form1_0.radioButton_10.Checked ? 1 : 0);
			form1_0.struct2_0.string_2 = form1_0.textBox_14.Text.Trim();
			form1_0.struct2_0.string_3 = form1_0.comboBox_16.Text.Trim();
			form1_0.struct2_0.int_15 = form1_0.comboBox_17.SelectedIndex;
			form1_0.struct2_0.int_16 = form1_0.comboBox_28.SelectedIndex;
			form1_0.struct2_0.string_4 = form1_0.textBox_13.Text.Trim();
			form1_0.struct2_0.int_0 = (form1_0.checkBox_6.Checked ? 1 : 0);
			form1_0.struct2_0.int_1 = (form1_0.checkBox_7.Checked ? 1 : 0);
			form1_0.struct2_0.string_0 = form1_0.textBox_11.Text;
			form1_0.struct2_0.int_3 = int.Parse(form1_0.textBox_7.Text);
			form1_0.struct2_0.int_4 = int.Parse(form1_0.textBox_10.Text);
			form1_0.struct2_0.int_5 = int.Parse(form1_0.textBox_9.Text);
			form1_0.struct2_0.int_2 = (form1_0.checkBox_8.Checked ? 1 : 0);
			form1_0.struct2_0.int_6 = (form1_0.checkBox_9.Checked ? 1 : 0);
			form1_0.struct2_0.int_7 = (form1_0.checkBox_14.Checked ? 1 : 0);
			form1_0.struct2_0.string_1 = form1_0.textBox_8.Text;
			form1_0.struct2_0.int_8 = form1_0.comboBox_19.SelectedIndex;
			form1_0.struct2_0.int_9 = form1_0.comboBox_15.SelectedIndex;
			form1_0.struct2_0.int_17 = (form1_0.TopMost ? 1 : 0);
			form1_0.struct3_0.int_14 = form1_0.Location.X;
			form1_0.struct3_0.int_15 = form1_0.Location.Y;
		}
	}
}
