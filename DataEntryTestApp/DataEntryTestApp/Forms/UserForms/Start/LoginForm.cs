using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEntryTestApp
{
    public partial class LoginForm : BaseForm
    {
        //base form behaviour
        public LoginForm()
        {
            InitializeComponent();
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            ExitAction();
        }
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            MinimizeAction();
        }
        private void FormHeader_MouseUp(object sender, MouseEventArgs e)
        {
            HeaderMouseUpAction();
        }
        private void FormHeader_MouseDown(object sender, MouseEventArgs e)
        {
           HeaderMouseDownAction();
        }
        private void FormHeader_MouseMove(object sender, MouseEventArgs e)
        {
            HeaderMouseMoveAction();
        }
        private void LoginSubmitButton_Click(object sender, EventArgs e)
        {
            DBConnect obj = new DBConnect();
            string username = usernameText.Text;
            string password = passwordText.Text;
            if (username != "" && password != "")
            {
                string login = "'" + username + "', '" + password + "'";
                obj.Insert("Account", "Username, Password", login);
            }
            if (true /*server confirms loggin details */)
            {
                this.Hide();
                ModeSelection modeForm = new ModeSelection();
                modeForm.ShowDialog();
                this.Close();
            }
        }
    }
}
