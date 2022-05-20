using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslationsCase.Models
{
    public class HomeModel 
    {
        private string _resultText;
            
        public string ResultText
        {
            get { return _resultText; }
            set { _resultText = value; }
        }

    }
}