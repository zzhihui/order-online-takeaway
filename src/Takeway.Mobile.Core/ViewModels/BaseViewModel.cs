using System;
using Cirrious.CrossCore.Core;

using Cirrious.MvvmCross.ViewModels;

namespace Takeway.Mobile.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set { _errorMessage = value; RaisePropertyChanged(() => ErrorMessage); }
        }


        private IGlobalModel _globalViewModel;
        public IGlobalModel GlobalViewModel
        {
            get
            {
                return _globalViewModel;
            }
            set { _globalViewModel = value; RaisePropertyChanged(() => GlobalViewModel); }
        }


        private string _confirmationMessage;
        public string ConfirmationMessage
        {
            get
            {
                return _confirmationMessage;
            }
            set { _confirmationMessage = value; RaisePropertyChanged(() => ConfirmationMessage); }
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }


        public void ShowViewModelScreen<TViewModel>()
            where TViewModel : BaseViewModel
        {
            ShowViewModel<TViewModel>();
        }



        public void Async(Action action, Action posAction=null)
        {
            IsBusy = true;
            

            MvxAsyncDispatcher.BeginAsync(() =>
                                        {

                                            action();
                                            this.InvokeOnMainThread(() =>
                                            {
                                                if(posAction!=null)
                                                posAction();

                                                IsBusy = false;
                                            });
                                        }

            );

        }
    }
}