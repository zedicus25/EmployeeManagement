using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client_Admin_.ViewModel
{
    public class HomePageVM : BaseVM
    {

        private CancellationTokenSource _tokenSource;
        private Task _vmCreation;
        private List<BaseVM> _allVMs;
        public HomePageVM()
        {
            _tokenSource = new CancellationTokenSource();
            _allVMs = new List<BaseVM>();
            _vmCreation = new Task(CreateVMs, _tokenSource.Token);
        }

        private async void CreateVMs()
        {
            while (MainVM.GetInstance().User == null)
                await Task.Delay(10);

            _tokenSource.Cancel();
        }
    }
}
