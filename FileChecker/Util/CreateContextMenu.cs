﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

namespace FileChecker.Util
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
            if (SelectedItemPaths.Count() == 1)
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
            var item = new ToolStripMenuItem("校验文件哈希值...");
            //添加事件
            item.Click += Item_Click;
            menu.Items.Add(item);
            return menu;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var filePath = SelectedItemPaths.First();
        }
    }
}
