using System;
using System.ComponentModel;
using System.Dynamic;
using System.Windows;
using Caliburn.Micro;
using Action = System.Action;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public class BaseViewModel : Screen
    {
        private static readonly ILog Logger = LogManager.GetLogger("BaseViewModel");



        private bool _selectionComplete;
        public bool SelectionComplete
        {
            get
            {
                return this._selectionComplete;
            }
            set
            {
                if (value != this._selectionComplete)
                {
                    this._selectionComplete = value;
                    NotifyOfPropertyChange(() => SelectionComplete);
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
            set
            {
                if (value == this._errorMessage) return;
                this._errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        private bool _onlineFeature;
        public bool OnlineFeature
        {
            get
            {
                return this._onlineFeature;
            }
            set
            {
                if (value == this._onlineFeature) return;
                this._onlineFeature = value;
                NotifyOfPropertyChange(() => OnlineFeature);
            }
        }

        private bool _posFeature;
        public bool PosFeature
        {
            get
            {
                return this._posFeature;
            }
            set
            {
                if (value == this._posFeature) return;
                this._posFeature = value;
                NotifyOfPropertyChange(() => PosFeature);
            }
        }



        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                if (value == this._isBusy) return;
                this._isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }


        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                NotifyOfPropertyChange(() => IsAdmin);

            }
        }









        public void OpenWindow(IWindowManager windowManager, IScreen optionExtraViewModel, string title, int? width = null, int? height = null)
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.Manual;

            settings.Width = width ?? 640;

            settings.Height = height ?? 480;
            settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            optionExtraViewModel.DisplayName = title;
            windowManager.ShowDialog(optionExtraViewModel, null, settings);
        }


        public void Async(Action action, Action final = null)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                action();
            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                IsBusy = false;
                if (final != null)
                    final();
            };
            IsBusy = true;
            worker.RunWorkerAsync();
        }


        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("WPFViewModel:[Execute] {0}-{1}", ex.Message, ex.StackTrace);
            }
        }

    }
}