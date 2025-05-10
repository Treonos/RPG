namespace RPG

{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void NextPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SavePage());
        }
    }

}