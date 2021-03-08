﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestingPlanner.Data;
using TestingPlanner.Models;

namespace TestingPlanner
{
    /// <summary>
    /// Interaction logic for RequestForm.xaml
    /// </summary>
    public partial class RequestForm : Window
    {
        // variables
        private DAO dao;
        private static Barco2021Context context = new Barco2021Context();

        // Constructor
        public RequestForm()
        {
            InitializeComponent();
            dao = DAO.Instance();
        }

        // The following functions are beign executed when the Request Form GUI is loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            addAllJobNaturesToCombobox();
            addAllDivionsToCombobox();
            fillInRequestDate();
            
        }

        
        // The following function is executed when the Confirm button is clicked on the GUI
        private void addJobRequest_Click(object sender, RoutedEventArgs e)
        {
            addRequest();
        }

        // We create an object from the DAO Class with the following parameters,
        // the value of these parameters is provided by the user using the Request Form GUI
        private void addRequest()
        {
            dao.addJobRequest("50", "pending", txtRequesterInit.Text, txtProjectName.Text, txtPartNr.Text, txtProjectNr.Text,
                              ifChecked(cbInternal), Convert.ToInt16(txtGrossWeight.Text), Convert.ToInt16(txtNetWeight.Text),
                              ifChecked(cbBatteries), txtLinkTestPlan.Text, txtSpecialRemarks.Text, cmbDivision.Text, cmbJobNature.Text,
                              dpEndDate.SelectedDate.Value.Date);
        }

        // This function retrieves the information of the job natures from the Barco2021 database and returns these
        // values in the correct combobox in the Request Form GUI
        private void addAllJobNaturesToCombobox()
        {
            var jobNatures = context.RqJobNature.ToList();
            foreach (RqJobNature jobNature in jobNatures)
            {
                cmbJobNature.Items.Add(jobNature.Nature);
            }
        }
        // This function retrieves the information of the divisions from the Barco2021 database and returns these values 
        // into the correct combobox in the Request Form GUI
        private void addAllDivionsToCombobox()
        {
            var divisions = context.RqBarcoDivision.ToList();
            foreach (RqBarcoDivision division in divisions)
            {
                cmbDivision.Items.Add(division.Afkorting);
            }
        }
        // This function automatically fills in the request date to the current day of the job request 
        private void fillInRequestDate()
        {
            txtRequestDate.Text = DateTime.Now.Date.ToShortDateString();
        }

        // If the Checkbox is checked we return the value : true and if the checkbox is not checked we return the value : false
        private bool ifChecked(CheckBox cb)
        {
            bool cbChecked = false;
            if (cb.IsChecked == true)
            {
                cbChecked = true;
            }
            return cbChecked;
        }

     
        //  txtSpecialRemarks.Text = dao.GetJobRequest().Requester;

    }
}
