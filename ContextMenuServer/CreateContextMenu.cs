using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

namespace ContextMenuServer
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFiles)]
    public class CreateContextMenu : SharpContextMenu
    {
        /// <summary>
        /// 是否显示上下文菜单
        /// </summary>
        /// <returns></returns>
        protected override bool CanShowMenu()
        {
            if (SelectedItemPaths.Count() == 1 && File.Exists(SelectedItemPaths.First()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 创建上下文菜单
        /// </summary>
        /// <returns></returns>
        protected override ContextMenuStrip CreateMenu()
        {
            var menu = new ContextMenuStrip();
            //要显示的文字
            var item = new ToolStripMenuItem("校验哈希值...");
            //添加事件
            item.Click += Item_Click;
            menu.Items.Add(item);
            return menu;
        }

        /// <summary>
        /// 右键菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Click(object sender, EventArgs e)
        {
            var filePath = SelectedItemPaths.First();
            var currentPath = Environment.CurrentDirectory;
            var appName = "FileChecker.exe";
            var fullPath = Path.Combine(currentPath, appName);
            Process.Start(fullPath, filePath);
        }
    }
}
