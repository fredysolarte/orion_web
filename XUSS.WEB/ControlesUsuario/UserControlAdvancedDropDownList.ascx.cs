using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.ComponentModel;
using System.Drawing.Design;

namespace XUSS.WEB.UserControls
{
    public partial class UserControlAdvancedDropDownList : System.Web.UI.UserControl
    {

        public string ValidationGroup
        {
            set
            {
                rtxtValue.ValidationGroup = value;
                RequiredFieldValidator1.ValidationGroup = value;
            }
        }

        public bool TextBoxVisible
        {
            set { rtxtValue.Visible = value; }
        }

        public string ErrorMessage
        {
            set { rcbDropDownList.ErrorMessage = value; }
        }

        public string Label
        {
            set { rtxtValue.Label = value; }
        }

        public string DataSourceID
        {
            set { rcbDropDownList.DataSourceID = value; }
        }

        public string DataTextField
        {
            set { rcbDropDownList.DataTextField = value; }
        }

        public string DataValueField
        {
            set { rcbDropDownList.DataValueField = value; }
        }

        public Unit TextBoxWidth
        {
            set { rtxtValue.Width = value; }
        }

        public Unit DropDownListWidth
        {
            set { rcbDropDownList.Width = value; }
        }

        public bool Enabled
        {
            set
            {
                rcbDropDownList.Enabled = value;
                rtxtValue.Enabled = value;
            }
        }

        public bool Validate
        {
            set { RequiredFieldValidator1.Enabled = value; }
        }

        private bool dataBound = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "";
        }

        public string SelectedValue
        {
            get { return string.IsNullOrEmpty(rcbDropDownList.SelectedValue) ? null : rcbDropDownList.SelectedValue; }
            set
            {
                    rcbDropDownList.SelectedValue = value;
                    rtxtValue.Text = value;
            }
        }

        protected void rtxtValue_TextChanged(object sender, EventArgs e)
        {
            if (rcbDropDownList.Items.FindItemIndexByValue(rtxtValue.Text) >= 0)
            {
                rcbDropDownList.SelectedValue = rtxtValue.Text;
                Label1.Text = "";
            }
            else
            {
                rtxtValue.Text = "";
                rcbDropDownList.SelectedValue = "";
                Label1.Text = "El codigo digitado no existe";
            }
        }

        protected void rcbDropDownList_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rtxtValue.Text = rcbDropDownList.SelectedValue;
            Label1.Text = "";
        }


        protected void rcbDropDownList_PreRender(object sender, EventArgs e)
        {
            if (rcbDropDownList.Items.Count == 0)
            {
                rtxtValue.Text = "";
            }
            else
            {
                if (rtxtValue.Text.Length != 0)
                {
                    if (rcbDropDownList.Items.FindItemIndexByValue(rtxtValue.Text) >= 0)
                    {
                        rcbDropDownList.SelectedValue = rtxtValue.Text;
                    }
                    else
                    {
                        rtxtValue.Text = "";
                        rcbDropDownList.SelectedValue = "";
                    }
                }
                if (dataBound)
                {
                    RadComboBoxItem tmp = new RadComboBoxItem("Seleccione un valor", null);
                    if (rtxtValue.Text.Length == 0)
                    {
                        tmp.Selected = true;
                        rcbDropDownList.Items.Insert(0, tmp);
                        rcbDropDownList.SelectedValue = null;
                        rcbDropDownList.SelectedIndex = 0;
                    }
                    else
                    {
                        rcbDropDownList.Items.Add(tmp);
                    }
                }
            }
        }

        protected void rcbDropDownList_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            dataBound = true;
        }
    }
}