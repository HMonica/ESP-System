﻿using System;
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
    public partial class StoreSales : BaseForm
    {
        Event eventRef;
        ModeSelection parentRef;

        //copy constructor
        public StoreSales(Event _eventRef, ModeSelection _parentRef)
        {
            eventRef = _eventRef;
            parentRef = _parentRef;
            InitializeComponent();
            PopulateListBox();
        }

        //adds all stores to the list box
        public void PopulateListBox()
        {
            foreach(Store foo in eventRef.stores)
            {
                ListBoxInsert(StoreListBox, foo.storeName);
            }
        }

        //checks if user has selected a store and opens relevant inventory, on complete, removes store from list
        private void StoreSelect_Click(object sender, EventArgs e)
        {
            ClearErrorLabel(ErrorLabel);
            if(CheckItemSelected(StoreListBox))
            {
                Store foo = eventRef.GetStore(StoreListBox.SelectedItem.ToString());
                using (var itemData = new ItemSales(foo.inventory, foo.storeName))
                {
                    this.Hide();
                    switch (itemData.ShowDialog())
                    {
                        case DialogResult.OK:
                            eventRef.SetStoreInventory(foo.storeName, itemData.inventoryRef);
                            ListBoxRemove(StoreListBox, StoreListBox.SelectedItem.ToString());
                            break;
                    }
                    this.Show();
                }
            }
            else
            {
                ErrorLabel.Text = "Error: No Store selected";
            }
        }

        //can only be clicked once all stores have sales data, sends data to server and sets event to complete
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if(StoreListBox.Items.Count == 0)
            {
                SalesCompleteMessage temp = new SalesCompleteMessage(eventRef, parentRef);
                this.Hide();
                temp.ShowDialog();
                this.Close();
            }
            else
            {
                SetError(ErrorLabel, "Not All Stores Complete");
            }
        }
    }
}
