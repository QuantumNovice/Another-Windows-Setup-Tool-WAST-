
using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;

Hosts hostfile = new Hosts();


//hostfile.addEntry("123.5.3.51", "test");

//hostfile.editEntry(4, "0.0.0.0", "owo.com");



string[] arguments = Environment.GetCommandLineArgs();

if (arguments.Length > 1)
{
    if (arguments[1] == "-l")
    {
        hostfile.readCLI();
    }
    else if (arguments.Length > 3 && arguments[1] == "-e")
    {
        int idx = int.Parse(arguments[2]);
        string ip = arguments[3]; //TODO: ip validator
        string host = arguments[4]; //TODO: hostname validator
        hostfile.editEntry(idx, ip, host);

    }
    else if (arguments.Length > 3 && arguments[1] == "-a")
    {
 
        string ip = arguments[2]; //TODO: ip validator
        string host = arguments[3]; //TODO: hostname validator
        hostfile.addEntry(ip, host);
    }
}
else
{
    Console.WriteLine(@"
WAST.exe:
=========
  -l                         --  Lists the contents of hostfile with indexes
  -e <index> <ip> <hostname> --  Edits hostfile at <index> with <ip> and <hostname>
  -a <ip> <hostname>         --  Appends <ip> <hostname> to hostsfile.
    ");
}
class Hosts {

    string path = "../../../hosts.txt";

    public IDictionary<int, string> readParsed()
    {
        /*
         * Reads hosts file and removes commented lines
         * 
         * returns Dict<int:line_num, string:line_content>
         */

        // Read Hosts File
        //string path = "C:/Windows/System32/Drivers/etc/hosts";
       
        string[] hosts = File.ReadAllLines(this.path);

        IDictionary<int, string> hosts_file = new Dictionary<int, string>();


        for (int i=0; i< hosts.Length; i++)
        {
            if (!hosts[i].StartsWith("#"))
            {
                hosts_file.Add(i, hosts[i]);
            }

        }
        return hosts_file;
    }

    public void addEntry(string ip, string domain)
    {
        /*
         * Appends Entry to a host file.
         */
        string comment = "comment";
        File.AppendAllText(this.path, $" {ip}\t{domain}\t\t # {comment}\n");
    }

    public void editEntry(int index, string ip, string domain)
    {
        /*
         * 
         */
        // TODO: don't use this
        string[] hosts = File.ReadAllLines(this.path);
        string comment = "comment";
        hosts[index] = $" {ip}\t{domain}\t\t # {comment}";
        File.WriteAllLines(this.path, hosts);
    }

    public void readCLI()
    {
        var no_comments = this.readParsed();

        foreach (var comment in no_comments)
        {
            Console.WriteLine($"{comment.Key} | {comment.Value}");
        }
    }

}



