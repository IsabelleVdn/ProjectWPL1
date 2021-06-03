﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingPlanner.Viewmodels
{
    class ViewModelStartupTester : ViewModelCollectionRQ
    {
        public ViewModelStartupTester() : base()
        {
            Load();
        }

        // Function used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            var requestIds = _dao.GetAllJobRequests();
            idRequestsOnly.Clear();

            foreach (var requestId in requestIds)
            {

                idRequestsOnly.Add(requestId);
            }
        }
    }
}