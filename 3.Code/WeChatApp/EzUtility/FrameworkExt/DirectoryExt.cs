using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EzUtility.FrameworkExt
{
    public static class DirectoryExt
    {
        public static void Copy(this DirectoryInfo info, string destDirName, bool copySubDirs)
        {
            if (info == null)
                return;

            DirectoryInfo[] dirs = info.GetDirectories();            

            if (!Directory.Exists(destDirName))            
                Directory.CreateDirectory(destDirName);

            FileInfo[] files = info.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    Copy(subdir, temppath, copySubDirs);
                }
            }
        }
    }
}
