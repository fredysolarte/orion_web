using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FACTURACION.SERVICIO
{
    public class FtpTransfer
    {
        public void SendFile(string filename, string ftpServerIP, string ftpUserName,string ftpPassword)
        {
            FileInfo objFile = new FileInfo(filename);

            FtpWebRequest objFTPRequest;

            // Create FtpWebRequest object 
            objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + objFile.Name));

            // Set Credintials
            objFTPRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

            // By default KeepAlive is true, where the control connection is 
            // not closed after a command is executed.
            objFTPRequest.KeepAlive = false;

            // Set the data transfer type.
            objFTPRequest.UseBinary = true;

            // Set content length
            objFTPRequest.ContentLength = objFile.Length;

            // Set request method
            objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;

            // Set buffer size
            int intBufferLength = 16 * 1024;
            byte[] objBuffer = new byte[intBufferLength];

            // Opens a file to read
            FileStream objFileStream = objFile.OpenRead();

            try
            {
                // Get Stream of the file
                Stream objStream = objFTPRequest.GetRequestStream();

                int len = 0;

                while ((len = objFileStream.Read(objBuffer, 0, intBufferLength)) != 0)
                {
                    // Write file Content 
                    objStream.Write(objBuffer, 0, len);

                }

                objStream.Close();
                objFileStream.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SendFileSTFP(string fileName,string host,string user,string password)
        {
            //var connectionInfo = new ConnectionInfo(host, "sftp", new PasswordAuthenticationMethod(user, password));
            var connectionInfo = new ConnectionInfo(host, user, new PasswordAuthenticationMethod(user, password));
            // Upload File
            using (var sftp = new SftpClient(connectionInfo))
            {

                sftp.Connect();
                sftp.ChangeDirectory(@"/INBOUND/invostream/xml");
                using (var uplfileStream = System.IO.File.OpenRead(fileName))
                {
                    sftp.UploadFile(uplfileStream, fileName, true);
                }
                sftp.Disconnect();
            }
            return 0;
        }
    }
}
