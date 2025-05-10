using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        // Créer un objet Random appelé randomizer
        // pour générer des nombres aléatoires.

        Random randomizer = new Random();

        // Ces variables entières stockent les nombres
        // pour le problème d'addition.

        int addend1;
        int addend2;

        // Ces variables entières stockent les nombres
        // pour le problème de soustraction. 
        int minuend;
        int subtrahend;

        // Ces variables entières stockent les nombres
        // pour le problème de multiplication.
        int multiplicand;
        int multiplier;

        // Ces variables entières stockent les nombres
        // pour le problème de division.
        int dividend;
        int divisor;

        // Cette variable entière garde une trace du 
        // temps restant.
        int timeLeft;

        /// <summary>
        /// Démarrer le quiz en remplissant tous les problèmes
        /// et en lançant le minuteur.
        /// </summary>
        public void StartTheQuiz()
        {
            // Remplir le problème d'addition.
            // Générer deux nombres aléatoires à additionner.
            // Stocker les valeurs dans les variables 'addend1' et 'addend2'.

            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convertir les deux nombres aléatoires générés
            // en chaînes de caractères afin qu'ils puissent être affichés
            // dans les contrôles d'étiquette.

            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' est le nom du contrôle NumericUpDown.
            // Cette étape garantit que sa valeur est zéro avant
            // d'y ajouter des valeurs.

            somme.Value = 0;

            // Remplir le problème de soustraction.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            différence.Value = 0;

            // Remplir le problème de multiplication.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            produit.Value = 0;

            // Remplir le problème de division.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Démarrer le minuteur.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        /// <summary>
        /// Vérifier les réponses pour voir si l'utilisateur a tout juste.
        /// </summary>
        /// <returns>True si la réponse est correcte, false sinon.</returns>

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == somme.Value)
                && (minuend - subtrahend == différence.Value)
                && (multiplicand * multiplier == produit.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // Si CheckTheAnswer() retourne true, alors l'utilisateur 
                // a toutes les bonnes réponses. Arrêter le minuteur  
                // et afficher une MessageBox.
                timer1.Stop();
                MessageBox.Show("Vous avez toutes les bonnes réponses !!", "Félicitations !!");
                startButton.Enabled = true;

            }else if (timeLeft > 0)
            {
                // Si CheckTheAnswer() retourne false, continuer 
                // le décompte. Diminuer le temps restant d'une seconde 
                // et afficher le nouveau temps restant en mettant à jour 
                // l'étiquette Time Left.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " secondes";
                if(timeLeft <= 5)
                {
                    //changer la couleur
                    timeLabel.BackColor = Color.Red;
                    Console.Beep();
                }
            }
            else
            {
                // Si l'utilisateur manque de temps, arrêter le minuteur, 
                // afficher une MessageBox et remplir les réponses.
                timer1.Stop();
                timeLabel.Text = "Temps écoulé !";
                MessageBox.Show("Vous n'avez pas terminé à temps.", "Dommage !");
                somme.Value = addend1 + addend2;
                différence.Value = minuend - subtrahend;
                produit.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Sélectionner la réponse entière dans le contrôle NumericUpDown.

            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
