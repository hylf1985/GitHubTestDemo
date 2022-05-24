using HZH_Controls.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.LightControl
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            InitMenu();
            ucMenu1.DataSource = menuItemEntities;
        }

        List<HZH_Controls.Controls.MenuItemEntity> menuItemEntities = new List<HZH_Controls.Controls.MenuItemEntity>();

        private void InitMenu()
        {
            menuItemEntities.Add(new MenuItemEntity() { Key = "1", Text = "主页" , Childrens = menuItemEntities});
            menuItemEntities.Add(new MenuItemEntity() { Key = "2", Text = "视觉设定" });
            menuItemEntities.Add(new MenuItemEntity() { Key = "3", Text = "运动设定" });
            menuItemEntities.Add(new MenuItemEntity() { Key = "4", Text = "信号监视" });
            menuItemEntities.Add(new MenuItemEntity() { Key = "5", Text = "管理员" });
            menuItemEntities.Add(new MenuItemEntity() { Key = "6", Text = "测试" });
           
           
        }

        List<string> menu = new List<string>() { "主页","视觉设定","管理员","测试","运动设定","信号监视"};
    }
}
