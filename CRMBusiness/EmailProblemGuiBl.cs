using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CRMBusiness.CRM;
using Ext.Net;

namespace CRMBusiness
{
    public class EmailProblemGuiBl
    {
        private int RowCount { get { return DataSource.Count; } }
        public int PageSize { get; set; }
        public List<EmailProblem> DataSource { get; set; }
        public ComponentDirectEvent.DirectEventHandler BtnLogOnDirectClick { get; set; }

        //returns a panel based on email problems design
        private Panel CreateEmailProblemPanel(EmailProblem ep, int index)
        {
            //create layout for panel
            var vboxLayout = new VBoxLayoutConfig
            {
                Align = VBoxAlign.Stretch
            };

            var fsDesc = new FieldSet
                             {
                                 ID = "fsDesc" + index,
                                 Title = "Description",
                                 Items =
                                     {
                                         new Label
                                             {
                                                 ID = "lblDesc" + index,
                                                 Text = ep.Description,
                                                 Margin = 10
                                             }
                                     }
                             };

            //create items for panel
            var btnLog = new Button
            {
                ID = "btnLog" + index,
                Text = "Create Ticket",
                Padding = 5,
                Margins = "0 13 10 0",
                Icon = Icon.ArrowEw
            };

            //add direct event for button
            btnLog.DirectEvents.Click.ExtraParams.Add(new Parameter("EP_ID", ep.EP_ID.ToString(CultureInfo.InvariantCulture)));
            btnLog.DirectEvents.Click.ExtraParams.Add(new Parameter("CLIENT_ID", ep.CLIENT_ID.ToString(CultureInfo.InvariantCulture)));
            btnLog.DirectClick += BtnLogOnDirectClick;

            //create the panel with properties
            var epDesignPanel = new Panel
            {
                ID = "pnlAcc" + index,
                BodyPadding = 10,
                Border = false,
                Title = "Date Sent: " + ep.DateCreated
            };

            //add the layout
            epDesignPanel.LayoutConfig.Add(vboxLayout);

            //add the items
            epDesignPanel.Items.Add(fsDesc);

            //if this problem has any images then create a button to display them
            var imageCount = new ImageBl().GetImageCount(ep.EP_ID);
            if (imageCount > 0)
            {
                var btnViewImage = new Button
                {
                    ID = "btnViewImage" + index,
                    Padding = 5,
                    Icon = Icon.PictureGo,
                    Text = "View Image",
                    Margins = "0 13 10 0",
                    OnClientClick = "window.open('ViewImages.aspx?epid=" + ep.EP_ID + "');"
                };
                epDesignPanel.Buttons.Add(btnViewImage);
            }

            epDesignPanel.Buttons.Add(btnLog);
   
            return epDesignPanel;
        }

        //returns a panel(panelCard) that will hold all the panels created by CreateEmailProblemPanel method
        private Panel CreateCardPanel(int index)
        {
            var accordionLayout = new AccordionLayoutConfig
            {
                Fill = false,
                Multi = true,
            };
            var cardPanel = new Panel
            {
                ID = "pnlCard" + index
            };
            cardPanel.LayoutConfig.Add(accordionLayout);

            return cardPanel;
        }

        //returns a list of cardPanels
        public List<Panel> CreateListOfCardPanels(out int numberOfProblems, out int numberOfPages)
        {
            var listOfPanels = new List<Panel>();
            var numOfCards = RowCount / PageSize;
            var rem = RowCount % PageSize;

            //data is less than pagsize so only need 1 card
            if (RowCount <= PageSize)
            {
                var cardPanel = CreateCardPanel(0);
                for (int i = 0; i < RowCount; i++)
                {
                    cardPanel.Items.Add(CreateEmailProblemPanel(DataSource[i], i));
                }
                listOfPanels.Add(cardPanel);
                numOfCards = 1;
            }
            else
            {
                //all cards have number of panels equal to pagesize
                if (rem == 0)
                {
                    for (var i = 0; i < numOfCards; i++)
                    {
                        //create a card
                        var cardPanel = CreateCardPanel(i);

                        //get start and end values for datasource
                        var start = i * PageSize;
                        var end = (PageSize * i) + PageSize;
                        
                        for (var j = start; j < end; j++)
                        {
                            cardPanel.Items.Add(CreateEmailProblemPanel(DataSource[j], j));
                        }
                        listOfPanels.Add(cardPanel);
                    }
                }
                else //The last card has number of panels less than pagesize
                {
                    numOfCards++;

                    for (var i = 0; i < numOfCards; i++)
                    {
                        //create a card
                        var cardPanel = CreateCardPanel(i);

                        //get start and end values for datasource
                        var start = i * PageSize;
                        
                        if (i == numOfCards - 1)
                        {
                            //set end to index of last item in datasource
                            //end += rem;
                            var end = (PageSize * i) + rem;

                            //add number of panels according to [rem]
                            for (var j = start; j < end; j++)
                            {
                                cardPanel.Items.Add(CreateEmailProblemPanel(DataSource[j], j));
                            }
                        }
                        else
                        {
                            var end = (PageSize * i) + PageSize;
                            for (var j = start; j < end; j++)
                            {
                                cardPanel.Items.Add(CreateEmailProblemPanel(DataSource[j], j));
                            }
                        }
                        listOfPanels.Add(cardPanel);
                    }
                    
                }
            }

            numberOfProblems = RowCount;
            numberOfPages = numOfCards;
            return listOfPanels;
        }
    }
}
