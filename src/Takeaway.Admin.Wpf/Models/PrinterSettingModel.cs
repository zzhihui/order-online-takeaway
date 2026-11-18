using Caliburn.Micro;

namespace Takeaway.Admin.Wpf.Models
{

    //TODO: Convert to WS Object
    public class PrinterSettingModel : Screen
    {
        private string _printerName;
        public string PrinterName
        {
            get { return _printerName; }
            set
            {
                _printerName = value;
                NotifyOfPropertyChange(() => PrinterName);

            }
        }

        private string _printerFontName;
        public string PrinterFontName
        {
            get { return _printerFontName; }
            set
            {
                _printerFontName = value;
                NotifyOfPropertyChange(() => PrinterFontName);

            }
        }

        private string _printerReceiptWidth;
        public string PrinterReceiptWidth
        {
            get { return _printerReceiptWidth; }
            set
            {
                _printerReceiptWidth = value;
                NotifyOfPropertyChange(() => PrinterReceiptWidth);

            }
        }

        private string _printerxFlow;
        public string PrinterxFlow
        {
            get { return _printerxFlow; }
            set
            {
                _printerxFlow = value;
                NotifyOfPropertyChange(() => PrinterxFlow);

            }
        }

        private string _printeryFlow;
        public string PrinteryFlow
        {
            get { return _printeryFlow; }
            set
            {
                _printeryFlow = value;
                NotifyOfPropertyChange(() => PrinteryFlow);

            }
        }

        private string _printerLargeSize;
        public string PrinterLargeSize
        {
            get { return _printerLargeSize; }
            set
            {
                _printerLargeSize = value;
                NotifyOfPropertyChange(() => PrinterLargeSize);

            }
        }

        private string _printerMediumSize;
        public string PrinterMediumSize
        {
            get { return _printerMediumSize; }
            set
            {
                _printerMediumSize = value;
                NotifyOfPropertyChange(() => PrinterMediumSize);

            }
        }


        private string _printerSmallSize;
        public string PrinterSmallSize
        {
            get { return _printerSmallSize; }
            set
            {
                _printerSmallSize = value;
                NotifyOfPropertyChange(() => PrinterSmallSize);

            }
        }


        private string _printerPrintWithGroup;
        public string PrinterPrintWithGroup
        {
            get { return _printerPrintWithGroup; }
            set
            {
                _printerPrintWithGroup = value;
                NotifyOfPropertyChange(() => PrinterPrintWithGroup);

            }
        }



        private string _printLanguage;
        public string PrintLanguage
        {
            get { return _printLanguage; }
            set
            {
                _printLanguage = value;
                NotifyOfPropertyChange(() => PrintLanguage);

            }
        }


        
    }
}