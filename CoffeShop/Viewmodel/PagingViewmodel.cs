using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;


namespace CoffeShop.Viewmodel
{
    public class PagingViewmodel : BindableBase
    {
        const int SizePagigng = 5;
        #region [Variable]
        private int _totalCountItem;
        private int _currentPage = 1;
        private int _skipItem;
        private Visibility _visibilityDotsUp;
        private Visibility _visibilityDotsDown;
        private bool _isEnableBtnPreviousPage;
        private bool _isEnableBtnNextPage;
        private int _totalCountPage;
        private int _gotoNumberPage;




        public int GotoNumberPage
        {
            get => _gotoNumberPage;
            set { _gotoNumberPage = value; OnPropertyChanged(); }
        }
        public int TotalCountPage
        {
            get => _totalCountPage;
            set { _totalCountPage = value; OnPropertyChanged(); }
        }
        public bool IsEnableBtnNextPage
        {
            get => _isEnableBtnNextPage;
            set { _isEnableBtnNextPage = value; OnPropertyChanged(); }
        }
        public bool IsEnableBtnPreviousPage
        {
            get => _isEnableBtnPreviousPage;
            set { _isEnableBtnPreviousPage = value; OnPropertyChanged(); }
        }
        public int CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(); }
        }
        public Visibility VisibilityDotsDown
        {
            get => _visibilityDotsDown;
            set { _visibilityDotsDown = value; OnPropertyChanged(); }
        }
        public Visibility VisibilityDotsUp
        {
            get => _visibilityDotsUp;
            set { _visibilityDotsUp = value; OnPropertyChanged(); }
        }
        public int TotalCountItem
        {
            get { return _totalCountItem; }
            set { _totalCountItem = value; OnPropertyChanged(); LoadNumberPage(value); GotoFirstNumber(); }
        }

        #endregion
        #region [Action]
        public Action<int, int> UpdateCollectionMethod { get; set; }
        #endregion
        #region [Collection]
        private ObservableCollection<Pagingner> _pageList;

        public ObservableCollection<Pagingner> PageList
        {
            get { return _pageList; }
            set { _pageList = value; OnPropertyChanged(); }
        }

        #endregion
        #region [Command]
        public ICommand SelectPageCMD { get { return new CommandHelper<Pagingner>((p) => { return p != null; }, SelectPage); } }
        public ICommand NextPageCMD { get { return new CommandHelper(NextPage); } }
        public ICommand PreviousPageCMD { get { return new CommandHelper(PreviousPage); } }
        public ICommand GotoFirstNumberCMD { get { return new CommandHelper(GotoFirstNumber); } }
        public ICommand GotoPageCMD
        {
            get
            {
                return new CommandHelper<string>((p) =>
                {

                    bool result = false;
                    if (p != null)
                    {
                        int value = 1;
                        result = int.TryParse(p.ToString(), out value);

                        if (result && value <= TotalCountPage && value > 0)
                        {
                            return true;
                        }
                    }
                    return false;

                }, GotoPage);
            }
        }
        #endregion


        public PagingViewmodel(Action<int, int> updateCollectionMethod, int pageSize = 10)
        {
            UpdateCollectionMethod = updateCollectionMethod;
            _skipItem = pageSize;
            PageList = new ObservableCollection<Pagingner>();
            VisibilityDotsDown = Visibility.Collapsed;
        }
        private void LoadNumberPage(int totalItem)
        {
            double totalCountPage = Math.Ceiling((double)totalItem / (double)_skipItem);
            if (totalCountPage == 0)
                totalCountPage = 1;
            TotalCountPage = (int)totalCountPage;
            PageList.Clear();

            for (int i = 0; i < totalCountPage; i++)
            {
                Pagingner pagingner = new Pagingner() { PageNumber = i + 1 };
                PageList.Add(pagingner);
                SetColorPagingner(pagingner);
                if (i == SizePagigng) break;
            }
            CheckLimitPage();
            HandleLogicUI();
        }
        private void ChangeNumberPageToUp(int startPage)
        {
            PageList.Clear();
            int j = 1;
            for (int i = startPage; true; i++)
            {
                if (startPage <= TotalCountPage)
                {
                    Pagingner pagingner = new Pagingner() { PageNumber = i };
                    PageList.Add(pagingner);
                    SetColorPagingner(pagingner);
                    if (j == SizePagigng) break;
                    j++;
                    startPage++;
                }
                else
                {
                    break;
                }
            }
        }
        private void ChangeNumberPageToDown(int startPage)
        {
            PageList.Clear();
            var listTemp = new List<Pagingner>();
            int j = 1;
            for (int i = startPage; true; i--)
            {
                if (startPage <= _totalCountPage)
                {
                    Pagingner pagingner = new Pagingner() { PageNumber = i };
                    listTemp.Add(pagingner);
                    SetColorPagingner(pagingner);
                    if (j == SizePagigng || i == 1) break;
                    j++;
                }
                else
                {
                    break;
                }
            }
            listTemp.Reverse(0, listTemp.Count);
            PageList = new ObservableCollection<Pagingner>(listTemp);
        }
        private void HandleLogicUI()
        {
            if (CurrentPage >= 7)
            {
                VisibilityDotsDown = Visibility.Visible;
            }
            else
            {
                VisibilityDotsDown = Visibility.Collapsed;
            }
            if (PageList.Count < _totalCountPage)
            {
                VisibilityDotsUp = Visibility.Visible;
            }
            else
            {
                VisibilityDotsUp = Visibility.Collapsed;
            }
        }
        private void SetColorPagingner(Pagingner pagingner)
        {
            if (pagingner != null)
            {
                if (pagingner.PageNumber == CurrentPage)
                {
                    SetDefaultColorPagingner();
                    pagingner.Background = new SolidColorBrush(Color.FromRgb(19, 84, 138));
                    pagingner.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
            }
        }
        private void SetDefaultColorPagingner()
        {
            PageList.ToList().ForEach(b => { b.Background = System.Windows.Media.Brushes.Transparent; b.Foreground = new SolidColorBrush(Color.FromRgb(19, 84, 138)); });
        }
        private void SelectPage(Pagingner pagingner)
        {
            if (PageList.IndexOf(pagingner) == 6)
            {
                if (pagingner.PageNumber < _totalCountPage)
                {
                    ChangeNumberPageToUp(pagingner.PageNumber);
                    if (PageList.Count > 0)
                        pagingner = PageList[0];
                }
            }
            CurrentPage = pagingner.PageNumber;
            SetColorPagingner(pagingner);
            CheckLimitPage();
            HandleLogicUI();
            UpdateCollectionMethod(CurrentPage, _skipItem);
        }
        private void NextPage()
        {
            CurrentPage++;
            var page = PageList.FirstOrDefault((p) => p.PageNumber == CurrentPage);
            if (PageList.IndexOf(page) == 6)
            {
                if (page.PageNumber < _totalCountPage)
                {
                    ChangeNumberPageToUp(page.PageNumber);
                    if (PageList.Count > 0)
                        page = PageList[0];
                }
            }
            SetColorPagingner(page);
            CheckLimitPage();
            HandleLogicUI();
            UpdateCollectionMethod(CurrentPage, _skipItem);
        }
        private void PreviousPage()
        {
            var seletedPage = PageList.FirstOrDefault((p) => p.PageNumber == CurrentPage);
            if (PageList.IndexOf(seletedPage) == 0)
            {
                if (seletedPage.PageNumber <= _totalCountPage)
                {
                    ChangeNumberPageToDown(CurrentPage);
                    if (PageList.Count >= 6)
                    {
                        seletedPage = PageList[5];
                        CurrentPage = seletedPage.PageNumber;
                    }
                    else
                    {
                        CurrentPage--;
                        seletedPage = PageList.FirstOrDefault((p) => p.PageNumber == CurrentPage);
                    }
                }
            }
            else
            {
                CurrentPage--;
                seletedPage = PageList.FirstOrDefault((p) => p.PageNumber == CurrentPage);
            }
            SetColorPagingner(seletedPage);
            CheckLimitPage();
            HandleLogicUI();
            UpdateCollectionMethod(CurrentPage, _skipItem);
        }
        private void CheckLimitPage()
        {
            if (CurrentPage <= _totalCountPage)
            {
                IsEnableBtnNextPage = true;
                if (CurrentPage == 0 && _totalCountPage == 1)
                {
                    IsEnableBtnNextPage = false;
                }
            }

            IsEnableBtnPreviousPage = CurrentPage > 1;
        }
        private void GotoPage(string gotoNumber)
        {
            int page = Convert.ToInt32(gotoNumber);
            if (_totalCountPage > 7)
                ChangeNumberPageToUp(page);

            CurrentPage = page;
            var pagingner = PageList.FirstOrDefault((p) => p.PageNumber == page);
            SetColorPagingner(pagingner);
            CheckLimitPage();
            HandleLogicUI();
            UpdateCollectionMethod(CurrentPage, _skipItem);
        }
        private void GotoFirstNumber()
        {
            ChangeNumberPageToUp(1);
            CurrentPage = 1;
            UpdateCollectionMethod(CurrentPage, _skipItem);
            SetColorPagingner(PageList[0]);
            HandleLogicUI();
        }
    }
    public class Pagingner : BindableBase
    {
        private int _pageNumber;
        private Brush _backGround;
        private Brush _foreground;




        public Brush Foreground
        {
            get => _foreground;
            set { _foreground = value; OnPropertyChanged(); }
        }
        public Brush Background
        {
            get => _backGround;
            set { _backGround = value; OnPropertyChanged(); }
        }
        public int PageNumber
        {
            get => _pageNumber;
            set { _pageNumber = value; OnPropertyChanged(); }
        }
        public Pagingner()
        {
            Foreground = new SolidColorBrush(Color.FromRgb(19, 84, 138));
            Background = System.Windows.Media.Brushes.Transparent;
        }
    }
}
