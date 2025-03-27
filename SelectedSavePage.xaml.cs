namespace RPG;

public partial class SelectedSavePage : ContentPage
{
	public SelectedSavePage()
	{
		InitializeComponent();
	}

    private async void NextPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewRun());
    }
}