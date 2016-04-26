using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bug_Lite.ViewModels
{
    public class StatisticsData
    {
        public int numIssues { get; set; }
        
        // Count Issues by CADD Software Product
        [DataType(DataType.Text)]
        public string caddSoftwareProduct { get; set; }
        public int issuesByCADDProduct { get; set; }

        // Count Issues by Issue Type
        [DataType(DataType.Text)]
        public string issueType { get; set; }
        public int issuesByType { get; set; }

        // Count Issues by Issue Priority
        [DataType(DataType.Text)]
        public string issuePriority { get; set; }
        public int issuesByPriority { get; set; }
    }
}