using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Util;

namespace FaxAPI_CSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var faxSimpleModel = new FaxOperations();
            var faxCompleModel = new FaxOperations();

            //Console.WriteLine("Sending Fax Simple Model ...");
            //faxSimpleModel.SendFaxSimpleModel();
            //Console.WriteLine("Enter any key to Send Fax Complex Model");
            //Console.ReadLine();
            //Console.WriteLine("Sending Fax Complex Model ...");
            //faxCompleModel.SendFaxComplexModel();
            Console.WriteLine("Enter any key to Get Fax Status, Detail, and Download Path");
            Console.ReadLine();
            Console.WriteLine("Get Fax Status ...");
            var FaxId = "208121775329";
            var FaxToDelete = "208121774837";
            faxSimpleModel.GetFaxStatus(FaxId);
            faxSimpleModel.GetFaxDetail(FaxId);
            faxSimpleModel.DownloadFax(FaxId);
            faxSimpleModel.DeleteFax(FaxToDelete);
            Console.WriteLine("Enter any key to get List of Fax by Folder Id");
            Console.ReadLine();
            Console.WriteLine("Get Fax List ...");
            var FolderId = "1003";
            var IsDownloaded = false;
            faxSimpleModel.GetFaxList(FolderId, IsDownloaded);
        }
    }
}

