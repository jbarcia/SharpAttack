﻿using System;
using System.Collections.Generic;
using SharpAttack.Utils;
using SharpSploit.Enumeration;

namespace SharpAttack.Commands
{
  class GetDomainComputer : Command
  {

    public override void Run(Dictionary<String, Parameter> RunParams)
    {

      Domain.DomainSearcher domainSearcher = new Domain.DomainSearcher();
      List<string> Computers = null;

      if (RunParams.TryGetValue("ComputerName", out Parameter computername))
      {
        Computers = computername.Value;
      }

      List<Domain.DomainObject> domainComputers = domainSearcher.GetDomainComputers(Computers);

      foreach (Domain.DomainObject computer in domainComputers)
      {
        Printing.TableHeader("Property", "Value");
        Printing.TableItem("Name", computer.name);
        Printing.TableItem("DistinguishedName", computer.distinguishedname);
        if (computer.serviceprincipalname.Length > 0)
        {
          string[] SPNs = computer.serviceprincipalname.Split(' ');
          foreach (string spn in SPNs)
          {
            Printing.TableItem("Service Principal Name", spn.TrimEnd(','));
          }
        }
      }
    }

    public GetDomainComputer()
    {
      Name = "GetDomainComputer";
      Helptext = "Returns information about a domain computer.";
      Parameters.Add("ComputerName", new Parameter("The computer name to lookup. Accepts a comma seperated list. If not specified, returns all computers in the domain.", 0));
      Register();
    }
  }
}
