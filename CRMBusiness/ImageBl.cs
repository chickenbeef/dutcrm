using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ImageBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);

        //Adds a new image to database
        public bool AddImage(byte[] imagefile, int epid)
        {
            if (imagefile.Equals(null) || epid.Equals(0)) return false;
            _crm = new CRMEntities(_uri);
            var i = new Image { ImageFile = imagefile, EP_ID = epid };
            _crm.AddToImages(i);
            _crm.SaveChanges();
            return true;
        }

        //Gets all images for a specific problem(EP_ID)
        public List<Image> GetImages(int epid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Images.Where(i => i.EP_ID == epid).ToList();
        }

        public int GetImageCount(int epid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Images.Where(i => i.EP_ID == epid).Count();
        }

        //Gets all images for a specific Ticket(CPR_ID) - clientproblemslog
        public List<Image> GetImagesForTicket(int cprid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Images.Where(i => i.EP_ID == cprid).ToList();
        }

        public int GetImageCountForTicket(int cprid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Images.Where(i => i.EP_ID == cprid).Count();
        }

        public void UpdateImage(int imgid, int cprid)
        {
            _crm = new CRMEntities(_uri);
            var image = _crm.Images.Where(i => i.IMG_ID == imgid).ToList()[0];
            image.CPR_ID = cprid;
            _crm.UpdateObject(image);
            _crm.SaveChanges();
        }
    }
}
