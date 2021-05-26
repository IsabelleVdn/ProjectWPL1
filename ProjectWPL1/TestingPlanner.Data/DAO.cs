﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using TestingPlanner.Classes;
using TestingPlanner;
using TestingPlanner.Domain.Models;



namespace TestingPlanner.Data
{
    // SINGLETON PATTERN
    // Private constructor, static instance
    // Ensures only one DBconnection is opened at a time
    // Ensures connection is closed when not in use
    public class DAO
    {
        // Variables
        private Barco2021Context context;
        private static readonly DAO instance = new DAO();

        public BarcoUser BarcoUser { get; }

        // Calls an DAO instance
        public static DAO Instance()
        {
            return instance;
        }

        // DAO Constructor - PRIVATE
        // Calls an instance from the Barco2021Context and stores this context in the current context
        private DAO()
        {
            this.context = new Barco2021Context();
            this.BarcoUser = RegistryConnection.GetValueObject<BarcoUser>(@"SOFTWARE\VivesBarco\Test");
        }


        // LISTS

        // Returns list of all JRs
        public List<RqRequest> GetAllJobRequests()
        {
            return context.RqRequest.Include(r => r.IdRequest).ToList();
        }

        // Returns list of all jobNatures
        public List<RqJobNature> GetAllJobNatures()
        {
            return context.RqJobNature.ToList();
        }

        // Returns list of all BarcoDivisions
        public List<RqBarcoDivision> GetAllDivisions()
        {
            return context.RqBarcoDivision.ToList();
        }

        // JR CHANGES

        /// <summary>
        /// Gets a JR with user data autofilled
        /// Kaat
        /// </summary>
        public JR GetNewJR()
        {
            JR autofilledJR = new JR()
            {
                Requester = BarcoUser.Name,
                BarcoDivision = BarcoUser.Division
            };

            return autofilledJR;
        }

        // INCOMPLETE
        // Creates and saves RqRequest based on JR
        // TODO: save data stored in other tables
        public RqRequest AddJobRequest(JR Jr)
        {
            // Copy data from JR to new RqRequest
            // Used ternary operator to use String.Empty when null
            RqRequest rqrequest = new RqRequest()
            {
                JrStatus = Jr.JrStatus == null ? string.Empty : Jr.JrStatus,
                RequestDate = Jr.ExpEnddate, // Nullable
                Requester = Jr.Requester == null ? string.Empty : Jr.Requester,
                BarcoDivision = Jr.BarcoDivision == null ? string.Empty : Jr.BarcoDivision,
                JobNature = Jr.JobNature == null ? string.Empty : Jr.JobNature,
                EutProjectname = Jr.EutProjectname == null ? string.Empty : Jr.EutProjectname,
                HydraProjectNr = Jr.HydraProjectnumber == null ? string.Empty : Jr.HydraProjectnumber,
                ExpectedEnddate = Jr.ExpEnddate == null ? DateTime.Now : (DateTime)Jr.ExpEnddate, // Not nullable, so needs to be casted
                InternRequest = Jr.InternRequest, // Bool, default false
                Battery = Jr.Battery // Bool, default false
            };

            return rqrequest;
        }

        //MOHAMED
        //Matti
        /// <summary>
        /// This function adds the input from the EUT part to the request object
        /// We create local variables to address the fields of the corresponding tables
        /// The combined object is eventually given to the context
        /// </summary>
        /// <param name="request"></param>
        /// <param name="eut"></param>
        public void AddEutToRqRequest(RqRequest request, EUT eut, string EutNr)
        {
            List<string> testDivision = new List<string>();

            request.GrossWeight = eut.GrossWeight == null ? string.Empty : eut.GrossWeight;
            request.NetWeight = eut.NetWeight == null ? string.Empty : eut.NetWeight;
            request.EutPartnumbers = request.EutPartnumbers == null ? string.Empty : request.EutPartnumbers;

            //We call the TestDivisionEutIsChecked function to check which testdivisions are checked
            TestDivisionEutIsChecked(eut, testDivision);

            // We link each testdivision to the corresponding id_request
            foreach (string testeut in testDivision)
            {
                var detail = new RqRequestDetail();
                detail.Testdivisie = testeut;
                detail.Euts.Add(new Eut
                {
                    OmschrijvingEut = "EUT" + EutNr,
                    AvailableDate = DateTime.Now
                });

                request.RqRequestDetails.Add(detail);
            };
            context.RqRequest.Add(request);
        }

        // INCOMPLETE
        // Finds RqRequest by ID, updates based on JR, and saves changes
        // Sends error message
        // TODO: update data stored in other tables
        public string UpdateJobRequest(JR Jr)
        {
            string message = null; // message is null on success
            // Error control
            // JR Number not empty?
            if (Jr.BarcoDivision != null)
            {
                RqRequest rqrequest = context.RqRequest.FirstOrDefault(r => r.IdRequest == Jr.IdRequest);

                rqrequest.JrNumber = Jr.JrNumber;
                rqrequest.JrStatus = Jr.JrStatus;
                rqrequest.RequestDate = Jr.RequestDate;
                rqrequest.Requester = Jr.Requester;
                rqrequest.BarcoDivision = Jr.BarcoDivision;
                rqrequest.JobNature = Jr.JobNature;
                rqrequest.EutProjectname = Jr.EutProjectname;
                rqrequest.HydraProjectNr = Jr.HydraProjectnumber;
                rqrequest.ExpectedEnddate = (DateTime)Jr.ExpEnddate; // Not nullable, so needs to be casted
                rqrequest.InternRequest = Jr.InternRequest;
                rqrequest.GrossWeight = Jr.GrossWeight;
                rqrequest.NetWeight = Jr.NetWeight;
                rqrequest.Battery = Jr.Battery;

                context.RqRequest.Update(rqrequest);
                SaveChanges();
            }
            else
            {
                message = "Error - empty job request\n" +
                    "Please fill in all necessary fields";
            }
            return message;
        }

        // INCOMPLETE
        // Gets existing JR by ID
        // TODO: catch nullRefEx - Currently impossible due to selecting listitem on load
        // TODO: link EUT's (via RqRequestDetail)
        // TODO: link RqOptionel
        public JR GetJRWithId(int idrequest)
        {
            // Find selected RqRequest
            RqRequest selectedRQ = context.RqRequest.FirstOrDefault(rq => rq.IdRequest == idrequest);

            // Create new JR with necessary data
            JR selectedJR = new JR
            {
                IdRequest = selectedRQ.IdRequest,
                JrNumber = selectedRQ.JrNumber,
                JrStatus = selectedRQ.JrStatus,
                RequestDate = selectedRQ.RequestDate,
                Requester = selectedRQ.Requester,
                BarcoDivision = selectedRQ.BarcoDivision,
                JobNature = selectedRQ.JobNature,
                EutProjectname = selectedRQ.EutProjectname,
                HydraProjectnumber = selectedRQ.HydraProjectNr,
                ExpEnddate = selectedRQ.ExpectedEnddate,
                InternRequest = selectedRQ.InternRequest,
                GrossWeight = selectedRQ.GrossWeight,
                NetWeight = selectedRQ.NetWeight,
                Battery = selectedRQ.Battery
            };

            return selectedJR;
        }
        /// <summary>
        /// This function creates a list of rqRequestDetails objects that are linked to the given idRequest via the parameter
        /// </summary>
        /// <param name="idrequest"></param>
        /// <returns></returns>
        public List<RqRequestDetail> rqDetail(int idrequest)
        {
            List<RqRequestDetail> DetailRQ = context.RqRequestDetails.Where(rq => rq.IdRequest == idrequest).ToList();
            return DetailRQ;
        }

        // SAVING
        // Stores all data from GUI in DB
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// This function checks which of the testdivision are checked via the user input
        /// If a test division is selected, we store this data in the test division list
        /// The user input is given via the eut object as a parameter
        /// </summary>
        private void TestDivisionEutIsChecked(EUT eut, List<string> testDivision)
        {
            // Kaat
            // Iterate over all properties of an EUT
            foreach (var property in typeof(EUT).GetProperties())
            {
                // Divisions are bools
                // Skip if the property is not a bool
                if (property.PropertyType == typeof(bool))
                {
                    // If the division is checked
                    if ((bool)property.GetValue(eut))
                    {
                        // Add the division to the list
                        testDivision.Add(property.Name);
                    }
                };
            }
        }
    }
}
