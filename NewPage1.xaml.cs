using System.Runtime.CompilerServices;

namespace RPG;

public partial class NewPage1 : ContentPage
{
    public NewPage1()
	{
        
		InitializeComponent();

        switch (GlobalPlayer.player1.Biome)
        {
            case "Desert":
                strona.BackgroundImageSource = "tlo_desert.png";
                enemy.Source = "raging_cobra.png";
                break;

            case "Mountains":
                strona.BackgroundImageSource = "tlo_mountains.png";
                enemy.Source = "glacial_golem.png";
                break;

            case "Castle":
                strona.BackgroundImageSource = "tlo_castle.png";
                enemy.Source = "pablo.png";
                break;

            case "Swamp":
                strona.BackgroundImageSource = "tlo_swamp.png";
                enemy.Source = "witch.png";
                break;
        }

        nick.Text = GlobalPlayer.player1.Nickname;

        switch (GlobalPlayer.player1.Class)
        {
            case "Knight":
                player.Source = "knight1.png";
                break;

            case "Archer":
                player.Source = "archer1.png";
                break;

            case "Mage":
                player.Source = "mage1.png";
                break;
        }
    }

    public void Attack(object sender, EventArgs e)
    {
        enemyHp.Progress -= 0.05;
        playerSp.Progress -= 0.03;
        switch (GlobalPlayer.player1.Class)
        {
            case "Knight":
                player.Source = "knight2.png";
                break;

            case "Archer":
                player.Source = "archer2.png";
                break;

            case "Mage":
                player.Source = "mage2.png";
                break;
        }



        switch (GlobalPlayer.player1.Class)
        {
            case "Knight":
                player.Source = "knight1.png";
                break;

            case "Archer":
                player.Source = "archer1.png";
                break;

            case "Mage":
                player.Source = "mage1.png";
                break;
        }
    }
}