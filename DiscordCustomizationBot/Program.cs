
using System.Net;
using System.Text;


/*
 *
 *  Developer: N*1 AKA LindaMosep
 *
 * 
*/
namespace Customization
{
    public class Token
    {
        public string token;
        public string mail;
        public string password;

        public Token(string token, string mail, string password)
        {
            this.token = token;
            this.mail = mail;
            this.password = password;

        }
    }

    public static class Program1
    {
        public static List<Token> tokens = new List<Token>();
        public static List<string> ImageBase64 = new List<string>();
        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        public static async Task MainAsync()
        {
            Console.WriteLine("Started");

            var list = File.ReadAllLines(Environment.CurrentDirectory + @"\tokens.txt");
            var nlist = File.ReadAllLines(Environment.CurrentDirectory + @"\ntokens.txt");
            var ips = File.ReadAllLines(Environment.CurrentDirectory + @"\ips.txt");
            var usernames = File.ReadAllLines(Environment.CurrentDirectory + @"\usernames.txt");
            foreach (var line in list)
            {
                if (line.Length > 1)
                {
                    try
                    {
                        var tkstring = line.Split(':').ToList();
                        if (tkstring.Count > 2)
                        {
                            var password = tkstring[1].Replace(":", "").Trim();

                            var mail = tkstring[0].Replace(":", "").Trim();


                            var token = tkstring[2].Replace(":", "").Trim();

                          

                            if (token.Length > 5)
                            {
                                if (nlist.Contains(token))
                                {
                                    tokens.Add(new Token(token, mail, password));
                                }
                               

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }


            }


            Console.WriteLine(tokens.Count);

            var images = System.IO.Directory.GetFiles(Environment.CurrentDirectory + @"\images");

            foreach (var image in images)
            {
            
                byte[] imageByte = System.IO.File.ReadAllBytes(image);
                string base64 = "data:image/png;base64," + Convert.ToBase64String(imageByte);
                ImageBase64.Add(base64);
            }


            #region Write Tokens
            
            foreach (var token in tokens)
            {
                Console.WriteLine(token.token);
            }
            

            #endregion

            #region Change pfp
            /* int imageInt = 0;
           for(int i = 0; i < tokens.Count; i++)
              {
                  if(!realNums.Contains(i))
                  {
                      ChangePfp(tokens[i].token, ips[i], ImageBase64[imageInt], i);

                      if (ImageBase64.Count - 1 > imageInt)
                      {
                          imageInt++;
                      }
                      else
                      {
                          imageInt = 0;
                      }

                      await Task.Delay(200);
                  }


              }
             */
            #endregion

            #region Change Username
            /*
          var nums =   File.ReadAllLines(Environment.CurrentDirectory + @"\aa.txt");
            var realnums = new List<int>();
            foreach(var num in nums)
            {
                realnums.Add(int.Parse(num));
            }
            int m = 0;
            int j = 0;
            for (int i = 0; i < tokens.Count; i++)
            {

                var username = "";
                var tempusername = usernames[i];
                if (tempusername.Length < 5)
                {
                    j++;
                    if (j <= 5)
                    {
                        tempusername +=  "*eth";

                    }
                    else if (j > 5 && j < 9)
                    {
                        tempusername += "*nft";
                    }
                    else
                    {
                        j = 0;
                        tempusername += "*eth";
                    }
                  
                }
                m++;
                if (m <= 5)
                {
                    username = tempusername + " | Space Doge";

                    
                }else if(m > 5)
                {
                    if(m < 8)
                    {
                        username = "Space Doge | " + tempusername;
                    }
                   
                  
                }
               

                if(m >= 8)
                {
                 
                        m = 0;
                        username = tempusername + " | Space Doge";
                    
                }
               
               
              if(!realnums.Contains(i))
                {
                    ChangeUsername(tokens[i].token, ips[i], username, tokens[i].password, i.ToString());

                    await Task.Delay(200);
                }


            }
          */
            #endregion


            await Task.Delay(-1);
        }

        public static async Task ChangePfp(string token, string proxySt, string base64, int i)
        {

            #region Proxy

            var sp = proxySt.Split(':');
            string proxyUser = sp[2].Replace(":", "").Trim();
            string proxyPass = sp[3].Replace(":", "").Trim();
            WebProxy myproxy = new(sp[0].Replace(":", "").Trim() + ":" + sp[1].Replace(":", "").Trim(), true);

            myproxy.Credentials= new NetworkCredential(proxyUser, proxyPass);

            #endregion

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me");
            request.Method = "PATCH";
            request.Headers.Add("Cookie", "__dcfduid=0214d34088ff11ecbefb770fdfec52a9; __sdcfduid=0214d34188ff11ecbefb770fdfec52a996964547316afd72d42cd8897bb667ee6582bcab026f769a38119512a9fe1cd8; locale=tr; __cf_bm=2wp5.R77sMVHBED4eqH9lz96poDHyPBPn66FirLYMDU-1644492890-0-AfnfnspfHKuTHj7tBzBOQD+bxfQIYXS4JJy184fDpNjPnmWnOTSm+//BCMDUPymBsLZDyscNGd5//CciFH1ubMS3R36AW3fBGka6h0m9RF9OBQI2oCLA1X4x4/eCZy5hzA==; OptanonConsent=isIABGlobal=false&datestamp=Thu+Feb+10+2022+14%3A34%3A50+GMT%2B0300+(GMT%2B03%3A00)&version=6.17.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false");
            request.Headers.Add("authorization", token);
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36";
            request.Headers.Add("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzk4LjAuNDc1OC44NyBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTguMC40NzU4Ljg3Iiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjExMzk3NywiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");
            request.ContentType = "application/json";



            request.Proxy = myproxy;


            var body = "{    \"avatar\":\"" + base64.Trim() + "\"    }";

            byte[] bytes = Encoding.UTF8.GetBytes(body);
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            try
            {
                var rp = await request.GetResponseAsync();
                var cd = new StreamReader(rp.GetResponseStream()).ReadToEnd();
                if (cd.ToLower().Contains("proxy"))
                {

                }
                else
                {
                    Console.WriteLine(i +" "+ token);
                }
            }
            catch (Exception ex)
            {
                int m = 0;
            }



        }

        public static async Task ChangeUsername(string token, string proxySt, string username, string password, string i)
        {

            #region Proxy

            var sp = proxySt.Split(':');
            string proxyUser = sp[2].Replace(":", "").Trim();
            string proxyPass = sp[3].Replace(":", "").Trim();
            WebProxy myproxy = new(sp[0].Replace(":", "").Trim() + ":" + sp[1].Replace(":", "").Trim(), true);

            myproxy.Credentials= new NetworkCredential(proxyUser, proxyPass);

            #endregion

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me");
            request.Method = "PATCH";
            request.Headers.Add("Cookie", "__dcfduid=0214d34088ff11ecbefb770fdfec52a9; __sdcfduid=0214d34188ff11ecbefb770fdfec52a996964547316afd72d42cd8897bb667ee6582bcab026f769a38119512a9fe1cd8; locale=tr; __cf_bm=2wp5.R77sMVHBED4eqH9lz96poDHyPBPn66FirLYMDU-1644492890-0-AfnfnspfHKuTHj7tBzBOQD+bxfQIYXS4JJy184fDpNjPnmWnOTSm+//BCMDUPymBsLZDyscNGd5//CciFH1ubMS3R36AW3fBGka6h0m9RF9OBQI2oCLA1X4x4/eCZy5hzA==; OptanonConsent=isIABGlobal=false&datestamp=Thu+Feb+10+2022+14%3A34%3A50+GMT%2B0300+(GMT%2B03%3A00)&version=6.17.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false");
            request.Headers.Add("authorization", token);
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36";
            request.Headers.Add("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzk4LjAuNDc1OC44NyBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTguMC40NzU4Ljg3Iiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjExMzk3NywiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");
            request.ContentType = "application/json";



            request.Proxy = myproxy;


            var body = "{    \"username\": \""+username+"\",    \"password\": \""+password+"\"}";

            byte[] bytes = Encoding.UTF8.GetBytes(body);
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            try
            {
                var rp = await request.GetResponseAsync();
                var cd = new StreamReader(rp.GetResponseStream()).ReadToEnd();
                if (cd.ToLower().Contains("proxy"))
                {

                }
                else
                {
                    Console.WriteLine(i);
                }
            }
            catch (Exception ex)
            {
                
            }



        }
    }
}