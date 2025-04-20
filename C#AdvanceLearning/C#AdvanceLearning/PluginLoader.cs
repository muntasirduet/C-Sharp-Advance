using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PaymentAbstractions;

namespace C_AdvanceLearning
{
    public static class PluginLoader
    {
        public static List<IPaymentPlugin> LoadPlugins(string pluginsRoot)
        {
            var plugins = new List<IPaymentPlugin>();
            if (!Directory.Exists(pluginsRoot))
            {
                Directory.CreateDirectory(pluginsRoot);
            }

            foreach (var dir in Directory.GetDirectories(pluginsRoot))
            {
                var dllPath = Directory.GetFiles(dir, "*.dll").FirstOrDefault();
                if (dllPath == null) continue;

                Assembly assembly = Assembly.LoadFrom(dllPath);
                var types = assembly.GetTypes()
                    .Where(t => typeof(IPaymentPlugin).IsAssignableFrom(t) && !t.IsAbstract);

                foreach (var type in types)
                {
                    var plugin = (IPaymentPlugin)Activator.CreateInstance(type);

                    // Optional: check versioning compatibility
                    if (plugin.CompatibleAppVersion == "1.0.0")
                    {
                        plugin.Install();
                        plugins.Add(plugin);
                    }
                }
            }

            return plugins;
        }
    }
}
