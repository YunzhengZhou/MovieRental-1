﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MovieRental
{
    public partial class UC2 : UserControl
    {
        private static UC2 _instance;
        public static UC2 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UC2();
                return _instance;
            }
        }
        public UC2()
        {
            InitializeComponent();
        }

        private void UC2_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(UC1.info);
            if (!rank.Controls.Contains(Ranking.Instance))
            {
                rank.Controls.Add(Ranking.Instance);
                Ranking.Instance.Dock = DockStyle.Fill;
                Ranking.Instance.BringToFront();
            }
            else
                Ranking.Instance.BringToFront();

            if (!YourMoviePanel.Controls.Contains(YourMovieControl.Instance))
            {
                YourMoviePanel.Controls.Add(YourMovieControl.Instance);
                YourMovieControl.Instance.Dock = DockStyle.Fill;
                YourMovieControl.Instance.BringToFront();
            }
            else
                YourMovieControl.Instance.BringToFront();

            if (!FeaturePanel.Controls.Contains(FeatureControl.Instance))
            {
                FeaturePanel.Controls.Add(FeatureControl.Instance);
                FeatureControl.Instance.Dock = DockStyle.Fill;
                FeatureControl.Instance.BringToFront();
            }
            else
                YourMovieControl.Instance.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] filename = { "", "god father", "mad max", "mary and max", "The love witch" };
            for (int i = 1; i < 5; i++)
            {
                MovieGroupBox newGroupBox = new MovieGroupBox();
                //newGroupBox.setGroupBox(YourMoviePanel, i);
                newGroupBox.setImage(newGroupBox.groupBox, filename[i]);
                newGroupBox.setMovieInfo(newGroupBox.groupBox, "God", "Nick", "Bento Box", "2018-02-11", "2018-05-03");
            }
            
        }

        private void Form2Tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(Form2Tab1.SelectedTab);
            if (Form2Tab1.SelectedTab == Form2Tab1.TabPages["Suggestion"])
            {
                MessageBox.Show("Suggestion");
            }
        }

        private void Suggestion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SUGGESTIONS");
        }
    }
}
