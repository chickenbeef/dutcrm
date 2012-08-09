using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ImageBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        //Adds a new image to database
        public void AddImage(byte[] imagefile, int epid)
        {
            _crm = new CRMEntities(_uri);
            var i = new Image {ImageFile = imagefile, EP_ID = epid};
            _crm.AddToImages(i);
            _crm.SaveChanges();
        }

        //Gets all images for a specific problem(EP_ID)
        public List<Image> GetImages(int epid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Images.Where(i => i.EP_ID == epid).ToList();
        }
    }
}
