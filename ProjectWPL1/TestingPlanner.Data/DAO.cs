﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestingPlanner.Domain.Models;



namespace TestingPlanner.Data
{
    public class DAO
    {
        // Variables
        private Barco2021Context context;
        private static readonly DAO instance = new DAO();

        // This functions calls an DAO instance
        public static DAO Instance()
        {
            return instance;
        }

        // DAO Constructor 
        // Calls an instance from the Barco2021Context and stores this context in the current context
        private DAO()
        {
            this.context = new Barco2021Context();
        }

        // This function creates a Job Request with the following data,
        // the data is beign retrieved from the requester using the GUI
        public RqRequest AddJobRequest(JR Jr)
        {
            RqRequest rqrequest = new RqRequest()
            {
                JrNumber = "test", // temporary name (hardcoded)
                JrStatus = Jr.JrStatus,
                RequestDate = DateTime.Now.Date,
                Requester = Jr.Requester,
                BarcoDivision = Jr.BarcoDivision,
                JobNature = Jr.JobNature == null ? string.Empty : Jr.JobNature,
                EutProjectname = Jr.EutProjectname == null ? string.Empty : Jr.EutProjectname,
                EutPartnumbers = Jr.EutPartnr == null ? string.Empty : Jr.EutPartnr,
                HydraProjectNr = Jr.HydraProjectnumber,
                ExpectedEnddate = DateTime.Now.Date,
                InternRequest = Jr.InternRequest,
                GrossWeight = Jr.GrossWeight == null ? string.Empty : Jr.GrossWeight,
                NetWeight = Jr.NetWeight == null ? string.Empty : Jr.NetWeight,
                Battery = Jr.Battery,
                RqOptionel = new List<RqOptionel> { new RqOptionel
                {
                    Link = Jr.Link,
                    Remarks = Jr.Remarks
                } },
                RqRequestDetail = new List<RqRequestDetail> {new RqRequestDetail
                {
                    Pvgresp = Jr.PvgResp,
                   // Testdivisie = "EMC",
                Eut = new List<Eut>{new Eut
                {
                    AvailableDate= DateTime.Now.Date
                }},
                    TestdivisieNavigation  = context.RqTestDevisions.FirstOrDefault(r => r.Afkorting == "z")
                    // TestdivisieNavigation = new RqTestDevision { Afkorting = "z"}} // Vervangen indien z nog niet bestaat
                }
                }
            };
            context.RqRequests.Add(rqrequest);
            SaveChanges();
            return rqrequest;
        }

        // This function returns all the job request and stores them in a list
        public List<RqRequest> GetAllJobRequests()
        {
            return context.RqRequests.Include(r => r.IdRequest).ToList();
            // return context.RqRequests.ToList();
        }

        public JR GetJRWithId(int idrequest)
        {
            // ToDo: EUT's (via RqRequestDetail)
            // ToDo: RqOptionel

            // Find selected RqRequest
            RqRequest selectedRQ = context.RqRequests.FirstOrDefault(rq => rq.IdRequest == idrequest);

            // Create new JR with necessary data
            JR selectedJR = new JR
            {
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

        //private string getPvgResp()
        //{
        //    var division = context.RqBarcoDivisionPersons
        //                   .Include(d => d.AfkPerson).Where(d => d.Pvggroup == PvgGroup)
        //                   .FirstOrDefault(d => d.AfkDevision == );
        //    return division.AfkPerson;
        //}


        // This function stores all the data from the GUI in the Barco2021 database
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
