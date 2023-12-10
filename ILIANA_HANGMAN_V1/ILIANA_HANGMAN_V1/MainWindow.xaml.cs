using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ILIANA_HANGMAN_V1
{
    public partial class MainWindow : Window
    {
        string MotRecherche; // Variable qui contiendra le mot à trouver
        char lettre, lettreTB; // Variables qui contiendra les charactères de la textbox et du mot à trouver
        string[] Mots; // Variable qui contiendra le tableau des mots à trouver
        int Lifes = 9; // Variable qui compte le nombre de vie 
        int Time = 60; // Variable qui compte le temps restant 
        int Score = 0; // Variable qui compte le score
        DispatcherTimer timer; // Timer de notre pendu
        
        // On initialise nos composants dans le "MainWindow"
        public  MainWindow()
        {
            InitializeComponent();
            // On créé ici un nouveau timer pour notre pendu à l'aide de la class "DispatcherTimer"
            timer = new DispatcherTimer();
            // On initialise l'intervale de temps entre chaque "Tick", ici notre timer avance seconde par seconde
            timer.Interval = new TimeSpan(0, 0, 1);
            // Grace à la méthode "EventHandler" on va appeler la voie "Timer_Tick", l'évenement se produit donc toutes les secondes
            timer.Tick += new EventHandler(Timer_Tick);
        }
        // On créé ici des varibles pour chacune de nos images grace au constructeur "URI"
        Uri Images1 = new Uri("Images/1.png", UriKind.Relative);
        Uri Images2 = new Uri("Images/2.png", UriKind.Relative);
        Uri Images3 = new Uri("Images/3.png", UriKind.Relative);
        Uri Images4 = new Uri("Images/4.png", UriKind.Relative);
        Uri Images5 = new Uri("Images/5.png", UriKind.Relative);
        Uri Images6 = new Uri("Images/6.png", UriKind.Relative);
        Uri Images7 = new Uri("Images/7.png", UriKind.Relative);
        Uri Images8 = new Uri("Images/8.png", UriKind.Relative);
        Uri ImagesMort = new Uri("Images/mort.png", UriKind.Relative);

        // Voie privée "Timer_Tick" qui est le programme qui va s'executer pour notre timer lorsque l'intervale du timer s'est écoulé (1 seconde)
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Le TexxtBlock "Timer" prends la valeur de notre variable "Time" que l'on convertit en type "string"
            Timer.Text = Time.ToString();
            // Décrémentation de notre variable "Time"
            Time--;
            // Instruction switch en fonction de la valeur de "Time"
            switch (Time)
            {
                // Si "Time" prends la valeur "14"
                case 14: // On enlève une seconde car le TexxtBlock Timer à un retard de 1seconde sur "Time" (le TBL_Time affichera "15")
                    // On change la couleur de notre TBL_Timer en rouge
                    Timer.Foreground = Brushes.Red;
                    break;
                // Si "Time" prends la valeur "-1" (le TBL_Time affichera "0")
                case -1:
                    // On arrète le timer
                    timer.Stop();
                    // On désactive les boutons 
                    Activate_Desactiverbtn(false);
                    // Notre image "Hangman" affichera l'image du pendu mort
                    Hangman.Source = new BitmapImage(ImagesMort);
                    // La TextBox Display affichera le message suivant
                    TB_Display.Text = $"Perdu ! Le mot était: {MotRecherche}";
                    break;
            }
            // On utilise la commande suivante pour éviter que notre jeu freeze
            CommandManager.InvalidateRequerySuggested();
        }
        // Voie privée "Activate_Desactiverbt" qui permet d'activer ou de désactiver les boutons lettres et le bouton indice
        private void Activate_Desactiverbtn(bool val)
        {
            // Ici on activera ou désactivera nos boutons selon la valeur qu'on leurs donnera plus tard (true ou false)
            BTN_A.IsEnabled = val;
            BTN_B.IsEnabled = val;
            BTN_C.IsEnabled = val;
            BTN_D.IsEnabled = val;
            BTN_E.IsEnabled = val;
            BTN_F.IsEnabled = val;
            BTN_G.IsEnabled = val;
            BTN_H.IsEnabled = val;
            BTN_I.IsEnabled = val;
            BTN_J.IsEnabled = val;
            BTN_K.IsEnabled = val;
            BTN_L.IsEnabled = val;
            BTN_M.IsEnabled = val;
            BTN_N.IsEnabled = val;
            BTN_O.IsEnabled = val;
            BTN_P.IsEnabled = val;
            BTN_Q.IsEnabled = val;
            BTN_R.IsEnabled = val;
            BTN_S.IsEnabled = val;
            BTN_T.IsEnabled = val;
            BTN_U.IsEnabled = val;
            BTN_V.IsEnabled = val;
            BTN_W.IsEnabled = val;
            BTN_X.IsEnabled = val;
            BTN_Y.IsEnabled = val;
            BTN_Z.IsEnabled = val;
            BTN_Indice.IsEnabled = val;
        }
       
        // Tableau "MotsFR" qui contient les mots à trouver en français
        string[] MotsFR = new string[]
        {"Chaussure","Horloge","Essence","Casque","Ivoire","Mutation","Nocturne","Bouteille","Soleil","Tentacule","Bague"};
        // Tableau "MotsEN" qui contient les mots à trouver en anglais
        string[] MotsEN = new string[]
        {"Laptop","Pen","bird","light","snow","mountain","computer","Desktop","keyboard","window","Digital","flower","spreadsheet"};

        // Voie privée de l'évenement "BTN_English_Click" qui se produit lorsque l'on clique sur le bouton "BTN_English"
        private void BTN_English_Click(object sender, RoutedEventArgs e)
        {
            // La varible "Mots" prends la valeur du tableau "MotsEN"
            Mots = MotsEN;
            // On désactive le bouton "BTN_English"
            BTN_EN.IsEnabled = false;
            // On active le bouton "BTN_Français"
            BTN_FR.IsEnabled = true;
        }
        // Voie privée de l'évenement "BTN_Français_Click" qui se produit lorsque l'on clique sur le bouton "BTN_Français"
        private void BTN_Français_Click(object sender, RoutedEventArgs e)
        {
            // Même fonctionement que pour le "BTN_English"
            Mots = MotsFR;
            BTN_FR.IsEnabled = false;
            BTN_EN.IsEnabled=true;
        }

        // Voie privée de l'évenement "BTN_Word_Click" qui se produit lorsque l'on clique sur le bouton "BTN_Word"
        private void BTN_Word_Click(object sender, RoutedEventArgs e)
        {
            // Le TextBlock "Score" affiche "0"
            TB_Score.Text = "0";
            // On lance le timer
            timer.Start();
            // La couleur du text du timer redevient noir
            Timer.Foreground = Brushes.Black;
            // On réinitialise la variable "Temps" à 0
            Time = 60;
            // On réinitialise la variable "Score" à 0
            Score = 0;
            // Si le joueur n'a appuyer ni sur le bouton anglais ni sur le bouton français et que la variable "Mots" est nulle
            if (Mots == null)
            {
                // Le jeu se lance par défaut en français, on utilise donc le tableau "MotsFR"
                Mots = MotsFR;
            }
            // "Rand" devient une variable aléatoire
            Random rand = new Random();
            // La variable "indice" prend donc l'indice d'un mot aléatoire dans notre tableau
            int indice = rand.Next(Mots.Length);
            // La variable "MotRecherche" devient le mot sélectionné dans notre tableau grace à son indice 
            MotRecherche = Mots[indice].ToUpper();
            // On réinitialise la variable "Lifes" à 9
            Lifes = 9;
            // On efface le contenu da la TB_Display
            TB_Display.Clear();
            // L'image Hangman est réinitialisé 
            Hangman.Source = null;
            // On active les boutons 
            Activate_Desactiverbtn(true);
            // Avec cette méthode l'utilisateur peut simplement lire le contenu de la "TB_Display" sans pouvoir intéragir avec (excepté avec les boutons)
            TB_Display.IsReadOnly = true;
            // Pour chaque lettre dans notre mot recherché
            for (int i = 0; i < MotRecherche.Length; i++)
            {
                // La Text Box Display affiche un "?"
                TB_Display.Text += "?";
            }
        }

        // Voie privée lorsque l'on clique sur l'un des boutons "Lettre"
        private void BTN_Lettre_Click(object sender, RoutedEventArgs e)
        {
            // contrôle "Button" qui réagit lorsque que l'on clique sur un bouton "Lettre"
            Button Lettre = sender as Button;
            // La variable "LettresT" prends la valeur du contenu du bouton "Lettre" sélectionné qu'on convertit en string
            string LettresT = Lettre.Content.ToString();
            // On désactive ensuite ce bouton Lettre 
            Lettre.IsEnabled = false;
            // Variable qui vérifie si la réponse est juste ou fausse, sa valeur par défaut est "false"
            bool Win = false; 
            // Tant que la valeur de "Lifes" et de "Time" n'est pas égale à 0
            if (Lifes != 0 && Time != 0)
            {
                // Boucle for pour chaque lettre "ltr" contenu dans le mot recherché    
                for (int ltr = 0; ltr < MotRecherche.Length; ltr++)
                {
                    // Si la lettre sélectionné "LettreT" est la même que la lettre "i" du mot recherché
                    if (MotRecherche.Substring(ltr, 1) == LettresT)
                    {
                        // La TB_Display.Text va enlever la lettre "ltr" de notre TextBox grâce à la méthode "Remove"
                        TB_Display.Text = TB_Display.Text.Remove(ltr, 1);
                        // La TB_Display.Text va remplacer la lettre "ltr" par la "LettresT" grâce à la méthode "Insert"
                        TB_Display.Text = TB_Display.Text.Insert(ltr, LettresT);
                        // La variable "win" prend la valeur "true" car une lettre à été remplacée
                        Win = true;
                        // Si le mot affiché dans la "TB_Display" est le même que le mot recherché
                        if (TB_Display.Text == MotRecherche)
                        {
                            // On arrète le timer
                            timer.Stop();
                            // On désactive les boutons
                            Activate_Desactiverbtn(false);
                            // L'utilisateur à gagné la partie et on affiche le message suivant
                            TB_Display.Text = $"Bien joué! '{MotRecherche}'";
                            // On multiplie par 10 le temps restant du timer et on l'ajoute au score
                            Score += Time * 10;
                        }
                    }
                }
                // Si la valeur de "Win" est "true"
                if (Win == true)
                {
                    // On ajoute 100 au score
                    Score += 100;
                    // On affiche le score dans "TB_Score"
                    TB_Score.Text = Score.ToString();
                }
                // Sinon (Win == false)
                else
                {
                    // On enlève 1 à la variable "Lifes"
                    Lifes--;
                    // Si le score n'est pas égal à 0
                    if (Score != 0)
                    {
                        // On enlève 25 au score
                        Score -= 25;
                        // On affiche le score dans "TB_Score"
                        TB_Score.Text = Score.ToString();
                    }
                }
            }
            // Si le score est plus élevé que le meilleur score
            if (Score > int.Parse(TB_MS.Text))
            {
                // On affiche ce score dans le TextBlock "Meilleur score"
                TB_MS.Text = Score.ToString();
            }
            // l'instruction "switch" affichera les images selon la valeur de la variable "Lifes"
            switch (Lifes)
            {
                // Par exemple si l'utilisateur perd une vie on affiche l'Image1 dans le champ image "Hangman"
                case 8:
                    Hangman.Source = new BitmapImage(Images1);
                    break;
                case 7:
                    Hangman.Source = new BitmapImage(Images2);
                    break;
                case 6:
                    Hangman.Source = new BitmapImage(Images3);
                    break;
                case 5:
                    Hangman.Source = new BitmapImage(Images4);
                    break;
                case 4:
                    Hangman.Source = new BitmapImage(Images5);
                    break;
                case 3:
                    Hangman.Source = new BitmapImage(Images6);
                    break;
                case 2:
                    Hangman.Source = new BitmapImage(Images7);
                    break;
                case 1:
                    Hangman.Source = new BitmapImage(Images8);
                    break;
                // Si la vie tombe à 0
                case 0:
                    // On arrète le timer
                    timer.Stop();
                    // On désactive les boutons
                    Activate_Desactiverbtn(false);
                    // On affiche l'image du pendu mort
                    Hangman.Source = new BitmapImage(ImagesMort);
                    // La TextBox Display affiche le message suivant 
                    TB_Display.Text = $"Perdu ! Le mot était: {MotRecherche}";
                    break;
            }
        }

        // Voie privée lorsque l'on clique sur le bouton "Indice"
        private void BTN_Indice_Click(object sender, RoutedEventArgs e)
        {
            // Variable qui vérifie si la lettre à été remplacée, sa valeur par défaut est "false"
            bool replace = false;
            // Tant que la valeur de "replace" est fausse
            while (replace == false)
            {
                // On créé un tableau "lTB" de charactères contentant les charactères de la "TB_Display"
                char[] lTB = TB_Display.Text.ToCharArray();
                // On créé un tableau "l" de charactères contentant les charactères du mot recherché
                char[] l = MotRecherche.ToCharArray();
                // "Rand" devient une variable aléatoire
                Random rand = new Random();
                // La variable "indice" prend l'indice d'un charactère aléatoire dans notre tableau "l"
                int indice = rand.Next(l.Length);
                // La variable "lettreTB" devient le charactère sélectionné dans le tableau "lTB"
                lettreTB = lTB[indice];
                // La variable "lettre" devient le charactère sélectionné dans le tableau "l"
                lettre = l[indice];
                // Pour chaque charactère "ltr" de notre mot recherché
                for (int ltr = 0; ltr < MotRecherche.Length; ltr++)
                {
                    // Si le char du mot recherché est le même que le char "lettre" 
                    // Et que le char "lettreTB" n'est pas le même que le char "lettre" (si ce charactère n'est pas déjà affiché dans la TB_Display)
                    if (MotRecherche.Substring(ltr, 1) == lettre.ToString() && lettreTB.ToString() != lettre.ToString())
                    {
                        // On remplace le char "ltr" par le char "lettre"
                        TB_Display.Text = TB_Display.Text.Remove(ltr, 1);
                        TB_Display.Text = TB_Display.Text.Insert(ltr, lettre.ToString());
                        // La variable "replace" prend la valeur "true"
                        replace = true;
                        // Si le contenu de notre "TB_Display" est égal au mot recherché
                        if (TB_Display.Text == MotRecherche)
                        {
                            // La "TB_Display" affiche le message suivant
                            TB_Display.Text = $"'{MotRecherche}' tricheur :(";
                            // On désactive les boutons
                            Activate_Desactiverbtn(false);
                            // On arrète le timer
                            timer.Stop();
                        }
                    }
                }
            }  
        }
    }
}
    

