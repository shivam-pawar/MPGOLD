using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Configuration;

namespace MPGOLDTUNCH
{
    public partial class Form : System.Windows.Forms.Form
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public Form()
        {
            InitializeComponent();
            customerName.Focus();
            SqlCommand cmd = new SqlCommand("select customer_name from customer_record", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            AutoCompleteStringCollection Collection = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                Collection.Add(dr.GetString(0));
            }
            customerName.AutoCompleteCustomSource = Collection;
            dr.Close();
            con.Close();
            string time = DateTime.Now.ToString("h:mm:ss tt");
            current_time.Text = time;
            refreshdata();
        }
        private void refreshdata()
        {

            SqlCommand cmd = new SqlCommand("select * from customer_record", con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dataGridRecords.DataSource = dt;
        }

        private void Preview(object sender, EventArgs e)
        {
            MPPrintPreviewDialog.Document = MPPrintDocument;
            MPPrintPreviewDialog.ShowDialog();
        }

        private void saveRecord()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO customer_record VALUES (@SrNo, @customer_name, @sample_type, @weight, @date, @gold)", con);
            cmd.Parameters.AddWithValue("@SrNo", int.Parse(srno.Text));
            cmd.Parameters.AddWithValue("@customer_name", customerName.Text);
            cmd.Parameters.AddWithValue("@sample_type", sampleType.Text);
            cmd.Parameters.AddWithValue("@weight", weight.Text);
            cmd.Parameters.AddWithValue("@date", datetimetext.Text);
            cmd.Parameters.AddWithValue("@gold", gold.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Inserted");
        }

        private void gold_value_changed(object sender, EventArgs e)
        {
            try
            {
                double gold_perent = double.Parse(gold.Text);
                double silver_perent = double.Parse(silver.Text);
                double cadmium_perent = double.Parse(cadmium.Text);
                double zinc_perent = double.Parse(zinc.Text);
                double iridium_perent = double.Parse(iridium.Text);
                double ruthenium_perent = double.Parse(ruthenium.Text);
                double osmium_perent = double.Parse(osmium.Text);
                double nickel_perent = double.Parse(nickel.Text);
                double tin_perent = double.Parse(tin.Text);
                double lead_perent = double.Parse(lead.Text);
                double platinum_perent = double.Parse(platinum.Text);
                double rhodium_perent = double.Parse(rhodium.Text);
                double iron_perent = double.Parse(iron.Text);
                double palladium_perent = double.Parse(palladium.Text);
                double cobalt_perent = double.Parse(cobalt.Text);
                double rhenium_perent = double.Parse(rhenium.Text);
                double tungsten_perent = double.Parse(tungsten.Text);
                double mangenese_perent = double.Parse(manganese.Text);
                double bismuth_perent = double.Parse(bismuth.Text);
                double karat_calculated = 0;
                try { karat_calculated = ((double)(gold_perent / 4.166666666666)); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                double copper_percent = 0;
                try
                {
                    copper_percent = ((double)(100.00 - gold_perent - silver_perent - cadmium_perent - zinc_perent - iridium_perent - ruthenium_perent - osmium_perent - nickel_perent - tin_perent - lead_perent - platinum_perent - rhodium_perent - iron_perent - palladium_perent - cobalt_perent - rhenium_perent - tungsten_perent - mangenese_perent - bismuth_perent));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Invalid Value entered");
                }

                copper.Text = ((double)(copper_percent)).ToString("0.00");
                karat.Text = ((double)(karat_calculated)).ToString("0.00");
                string time = DateTime.Now.ToString("h:mm:ss tt");
                current_time.Text = time;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void search_customer_name(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from customer_record where customer_name LIKE '%'+@customer_name+'%'", con);
            cmd.Parameters.AddWithValue("@customer_name", search_customer.Text);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dataGridRecords.DataSource = dt;
        }

        private void search_date_record(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from customer_record where date=@date", con);
            cmd.Parameters.AddWithValue("@date", search_date.Text);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dataGridRecords.DataSource = dt;
        }

        private void reset_form()
        {
            search_customer.Text = null;
            weight.Text = (0.000).ToString("0.000");
            gold.Text = (0.00).ToString("0.00");
            silver.Text = (0.00).ToString("0.00");
            cadmium.Text = (0.00).ToString("0.00");
            zinc.Text = (0.00).ToString("0.00");
            iridium.Text = (0.00).ToString("0.00");
            ruthenium.Text = (0.00).ToString("0.00");
            osmium.Text = (0.00).ToString("0.00");
            nickel.Text = (0.00).ToString("0.00");
            tin.Text = (0.00).ToString("0.00");
            lead.Text = (0.00).ToString("0.00");
            platinum.Text = (0.00).ToString("0.00");
            iron.Text = (0.00).ToString("0.00");
            rhodium.Text = (0.00).ToString("0.00");
            palladium.Text = (0.00).ToString("0.00");
            cobalt.Text = (0.00).ToString("0.00");
            rhenium.Text = (0.00).ToString("0.00");
            tungsten.Text = (0.00).ToString("0.00");
            manganese.Text = (0.00).ToString("0.00");
            bismuth.Text = (0.00).ToString("0.00");
            karat.Text = (0.00).ToString("0.00");
            sampleType.Text = "Dhali";
        }

        private void ResetForm(object sender, EventArgs e)
        {
            refreshdata();
        }

        private void SaveAndPrint(object sender, EventArgs e)
        {
            saveRecord();
            PrintDialog p1 = new PrintDialog();
            PrintDocument p2 = new PrintDocument();
            p2.DefaultPageSettings.PaperSize= new PaperSize("210 x 297 mm", 800, 800);

            p2.DocumentName = "Print Document";
            p1.Document = p2;
            p1.AllowSelection = true;
            p1.AllowSomePages = true;

            if(p1.ShowDialog()== DialogResult.OK)
            {
                MPPrintDocument.Print();
            }
            refreshdata();
            reset_form();
        }

        private void MPPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int nintyX = 90;
            int fourNintyX = 490;
            int twoNintyX = 290;
            int twoNintyY = 290;
            int oneNintyX = 190;
            int oneNintyY = 190;
            int oneFiftyY = 150;
            int threeFifteenX = 315;
            int eightyFiveX = 85;
            int eightEightyFiveY = 885;
            int twoFiftyY = 250;
            int fourSixtyX = 460;
            int threeZeroZeroY = 300;
            int fifeNintyX = 590;
            int twoThirtyY = 230;
            int sevenFifteenX = 715;
            int threeTwoFiveY = 325;
            int threeFiveZeroY = 350;
            int threeSevenFiveY = 375;
            int fourZeroZeroY = 400;
            int fourTwoFiveY = 425;
            int threeNineZeroX = 390;
            int nineThreeFiveY = 935;
            int nineOneZeroY = 910;
            int eightSixZeroY = 860;
            int eightThreeFiveY = 835;
            int eightOneZeroY = 810;
            int sixSixZeroY = 660;
            int sevenZeroZeroY = 700;
            int sevenSixZeroY = 760;
            int twoSevenZeroX = 270;
            int fourFourSevenY = 447;
            int sevenFourZeroY = 740;
            int eightZeroZeroY = 800;
            int nineFiveSevenY = 957;

            Point p1 = new Point(eightyFiveX, twoThirtyY);
            Point p2 = new Point(sevenFifteenX, twoThirtyY);
            string time = DateTime.Now.ToString("h:mm:ss tt");
            current_time.Text = time;
            e.Graphics.DrawString("Name : "+customerName.Text, new Font("Arial", 12, FontStyle.Regular),Brushes.Black, new Point(nintyX, oneFiftyY));
            e.Graphics.DrawString("Time : " + current_time.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(threeFifteenX, oneFiftyY));
            e.Graphics.DrawString("Serial No. : " + srno.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(fourNintyX, oneFiftyY));
            e.Graphics.DrawString("Date : " +datetimetext.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(nintyX, oneNintyY));
            e.Graphics.DrawString("Sample : " + sampleType.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(threeFifteenX, oneNintyY));

            e.Graphics.DrawLine(new Pen(Color.Black, 2), p1, p2);

            e.Graphics.DrawString("GOLD : " + gold.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Black, new Point(nintyX, twoFiftyY));
            e.Graphics.DrawString("KARAT : " + karat.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(twoNintyX, twoFiftyY));
            e.Graphics.DrawString("WEIGHT : " + weight.Text+" gram", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(fourSixtyX, twoFiftyY));

            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(eightyFiveX, twoNintyY), new Point(sevenFifteenX, twoNintyY));

            e.Graphics.DrawString("Copper", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, threeZeroZeroY));
            e.Graphics.DrawString("Silver", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, threeTwoFiveY));
            e.Graphics.DrawString("Zinc", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, threeFiveZeroY));
            e.Graphics.DrawString("Cadmium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, threeSevenFiveY));
            e.Graphics.DrawString("Lead", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, fourZeroZeroY));
            e.Graphics.DrawString("Nickel", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, fourTwoFiveY));

            e.Graphics.DrawString(": " + " " + copper.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, threeZeroZeroY));
            e.Graphics.DrawString(": " + " " + silver.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, threeTwoFiveY));
            e.Graphics.DrawString(": " + " " + zinc.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, threeFiveZeroY));
            e.Graphics.DrawString(": " + " " + cadmium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, threeSevenFiveY));
            e.Graphics.DrawString(": " + " " + lead.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, fourZeroZeroY));
            e.Graphics.DrawString(": " + " " + nickel.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, fourTwoFiveY));

            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(twoSevenZeroX, twoNintyY), new Point(twoSevenZeroX, fourFourSevenY));

            e.Graphics.DrawString("Iridium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, threeZeroZeroY));
            e.Graphics.DrawString("Ruthenium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, threeTwoFiveY));
            e.Graphics.DrawString("Osmium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, threeFiveZeroY));
            e.Graphics.DrawString("Tin", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, threeSevenFiveY));
            e.Graphics.DrawString("Rhodium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, fourZeroZeroY));
            e.Graphics.DrawString("Iron", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, fourTwoFiveY));

            e.Graphics.DrawString(": " + " " + iridium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, threeZeroZeroY));
            e.Graphics.DrawString(": " + " " + ruthenium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, threeTwoFiveY));
            e.Graphics.DrawString(": " + " " + osmium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, threeFiveZeroY));
            e.Graphics.DrawString(": " + " " + tin.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, threeSevenFiveY));
            e.Graphics.DrawString(": " + " " + rhodium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, fourZeroZeroY));
            e.Graphics.DrawString(": " + " " + iron.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, fourTwoFiveY));

            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(fourSixtyX, twoNintyY), new Point(fourSixtyX, fourFourSevenY));

            e.Graphics.DrawString("Palladium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, threeZeroZeroY));
            e.Graphics.DrawString("Cobalt", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, threeTwoFiveY));
            e.Graphics.DrawString("Rhenium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, threeFiveZeroY));
            e.Graphics.DrawString("Tungsten", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, threeSevenFiveY));
            e.Graphics.DrawString("Manganese", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, fourZeroZeroY));
            e.Graphics.DrawString("Bismuth", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, fourTwoFiveY));

            e.Graphics.DrawString(": " + " " + palladium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, threeZeroZeroY));
            e.Graphics.DrawString(": " + " " + cobalt.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, threeTwoFiveY));
            e.Graphics.DrawString(": " + " " + rhenium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, threeFiveZeroY));
            e.Graphics.DrawString(": " + " " + tungsten.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, threeSevenFiveY));
            e.Graphics.DrawString(": " + " " + manganese.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, fourZeroZeroY));
            e.Graphics.DrawString(": " + " " + bismuth.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, fourTwoFiveY));

            //Second Form Copy

            Point p3 = new Point(eightyFiveX, sevenFourZeroY);
            Point p4 = new Point(sevenFifteenX, sevenFourZeroY);
            e.Graphics.DrawString("Name : " + customerName.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(nintyX, sixSixZeroY));
            e.Graphics.DrawString("Serial No. : " + srno.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(fourNintyX, sixSixZeroY));
            e.Graphics.DrawString("Time : " + current_time.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(threeFifteenX, sixSixZeroY));
            e.Graphics.DrawString("Date : " + datetimetext.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(nintyX, sevenZeroZeroY));
            e.Graphics.DrawString("Sample : " + sampleType.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(threeFifteenX, sevenZeroZeroY));

            e.Graphics.DrawLine(new Pen(Color.Black, 2), p3, p4);

            e.Graphics.DrawString("GOLD : " + gold.Text, new Font("Arial", 17, FontStyle.Bold), Brushes.Black, new Point(nintyX, sevenSixZeroY));
            e.Graphics.DrawString("KARAT : " + karat.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(twoNintyX, sevenSixZeroY));
            e.Graphics.DrawString("WEIGHT : " + weight.Text + " gram", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(fourSixtyX, sevenSixZeroY));

            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(eightyFiveX, eightZeroZeroY), new Point(sevenFifteenX, eightZeroZeroY));

            e.Graphics.DrawString("Copper", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, eightOneZeroY));
            e.Graphics.DrawString("Silver", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, eightThreeFiveY));
            e.Graphics.DrawString("Zinc", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, eightSixZeroY));
            e.Graphics.DrawString("Cadmium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, eightEightyFiveY));
            e.Graphics.DrawString("Lead", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, nineOneZeroY));
            e.Graphics.DrawString("Nickel", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(nintyX, nineThreeFiveY));

            e.Graphics.DrawString(": " + " " + silver.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, eightThreeFiveY));
            e.Graphics.DrawString(": " + " " + zinc.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, eightSixZeroY));
            e.Graphics.DrawString(": " + " " + cadmium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, eightEightyFiveY));
            e.Graphics.DrawString(": " + " " + lead.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, nineOneZeroY));
            e.Graphics.DrawString(": " + " " + nickel.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(oneNintyX, nineThreeFiveY));

            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(twoSevenZeroX, eightZeroZeroY), new Point(twoSevenZeroX, nineFiveSevenY));

            e.Graphics.DrawString("Iridium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, eightOneZeroY));
            e.Graphics.DrawString("Ruthenium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, eightThreeFiveY));
            e.Graphics.DrawString("Osmium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, eightSixZeroY));
            e.Graphics.DrawString("Tin", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, eightEightyFiveY));
            e.Graphics.DrawString("Rhodium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, nineOneZeroY));
            e.Graphics.DrawString("Iron", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(twoNintyX, nineThreeFiveY));

            e.Graphics.DrawString(": " + " " + iridium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, eightOneZeroY));
            e.Graphics.DrawString(": " + " " + ruthenium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, eightThreeFiveY));
            e.Graphics.DrawString(": " + " " + osmium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, eightSixZeroY));
            e.Graphics.DrawString(": " + " " + tin.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, eightEightyFiveY));
            e.Graphics.DrawString(": " + " " + rhodium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, nineOneZeroY));
            e.Graphics.DrawString(": " + " " + iron.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(threeNineZeroX, nineThreeFiveY));

            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(fourSixtyX, eightZeroZeroY), new Point(fourSixtyX, nineFiveSevenY));

            e.Graphics.DrawString("Palladium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, eightOneZeroY));
            e.Graphics.DrawString("Cobalt", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, eightThreeFiveY));
            e.Graphics.DrawString("Rhenium", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, eightSixZeroY));
            e.Graphics.DrawString("Tungsten", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, eightEightyFiveY));
            e.Graphics.DrawString("Manganese", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, nineOneZeroY));
            e.Graphics.DrawString("Bismuth", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fourNintyX, nineThreeFiveY));

            e.Graphics.DrawString(": " + " " + palladium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, eightOneZeroY));
            e.Graphics.DrawString(": " + " " + cobalt.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, eightThreeFiveY));
            e.Graphics.DrawString(": " + " " + rhenium.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, eightSixZeroY));
            e.Graphics.DrawString(": " + " " + tungsten.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, eightEightyFiveY));
            e.Graphics.DrawString(": " + " " + manganese.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, nineOneZeroY));
            e.Graphics.DrawString(": " + " " + bismuth.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(fifeNintyX, nineThreeFiveY)); 
        }
    }
}
