using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Mime;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Collections;


namespace ImapPackage
{
    /// <summary>
    /// Interaction logic for CreateAndSend.xaml
    /// </summary>
    public partial class CreateAndSend : Page 
    {
        
        ArrayList attachments;
        public CreateAndSend()
        {
            InitializeComponent();
            
        }
        
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dialog.ShowDialog();
            if (result==true)
            {
                this.attachments.Add(dialog.FileName);
                this.attachmentsView.Items.Add(new My_Item { Name = dialog.FileName, Size = ((double)(new FileInfo(dialog.FileName).Length / 1000)).ToString("f2") + " KB" });
            }
        }
        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            
            using (SmtpClient smtpclient = new SmtpClient("smtp.gmail.com", 25))
            {
                
                smtpclient.EnableSsl = true;
                smtpclient.Credentials = new NetworkCredential(HomePage.mail,HomePage.password);
                // client.UseDefaultCredentials = true;

                
                MailMessage message = new MailMessage(
                                         HomePage.mail, // From field
                                         to.Text, // Recipient field
                                         subject.Text, // Subject of the email message
                                         body.Text // Email message body
                                      );
                if (attachments.Capacity != 0)
                {
                   
                    foreach (string attach in attachments)
                    {
                        var data = new Attachment(attach, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(attach);
                        disposition.ModificationDate = File.GetLastWriteTime(attach);
                        disposition.ReadDate = File.GetLastAccessTime(attach);
                        message.Attachments.Add(data);
                    }
                   smtpclient.Send(message);

                    HomePage.ClearRoom();  
                }
            }
        }
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            HomePage.ClearRoom();
        }
    }
}
