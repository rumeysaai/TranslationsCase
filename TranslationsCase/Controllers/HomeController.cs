using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TranslationsCase.Models;

namespace TranslationsCase.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.ResultText = "";
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            HomeModel model = new HomeModel();
            string eng = form["eng"].Trim();

            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>();
            values.Add("text", eng);

            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync("https://api.funtranslations.com/translate/valyrian.json", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            JObject o = JObject.Parse(responseString);

            if (o["contents"] != null)
                model.ResultText = o["contents"]["translated"].Value<string>();
            else
                model.ResultText = o["error"]["message"].Value<string>();

            TransCaseEntities db = new TransCaseEntities();
            Log modell = new Log();
            modell.Calls = form["eng"].Trim();
            modell.Results = responseString;
            modell.Time = DateTime.Now;
            db.Logs.Add(modell);
            db.SaveChanges();

            return View(model);
        }
    }
}