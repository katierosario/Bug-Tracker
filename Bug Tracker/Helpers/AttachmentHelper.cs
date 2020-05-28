using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Bug_Tracker.Helpers
{
    public class AttachmentHelper
    {
        public static string ShowIcon(string filePath)
        {
            string iconPath;
            switch (Path.GetExtension(filePath))
            {
                case ".aac":
                    iconPath = "/Images/FileTypeIcons/aac.png";
                    break;
                case ".ai":
                    iconPath = "/Images/FileTypeIcons/ai.png";
                    break;
                case ".aiff":
                    iconPath = "/Images/FileTypeIcons/aiff.png";
                    break;
                case ".asp":
                    iconPath = "/Images/FileTypeIcons/asp.png";
                    break;
                case ".avi":
                    iconPath = "/Images/FileTypeIcons/avi.png";
                    break;
                case ".blank":
                    iconPath = "/Images/FileTypeIcons/blank.png";
                    break;
                case ".bmp":
                    iconPath = "/Images/FileTypeIcons/bmp.png";
                    break;
                case ".c":
                    iconPath = "/Images/FileTypeIcons/c.png";
                    break;
                case ".cpp":
                    iconPath = "/Images/FileTypeIcons/cpp.png";
                    break;
                case ".css":
                    iconPath = "/Images/FileTypeIcons/css.png";
                    break;
                case ".dat":
                    iconPath = "/Images/FileTypeIcons/dat.png";
                    break;
                case ".dmg":
                    iconPath = "/Images/FileTypeIcons/dmg.png";
                    break;
                case ".doc":
                    iconPath = "/Images/FileTypeIcons/doc.png";
                    break;
                case ".docx":
                    iconPath = "/Images/FileTypeIcons/docx.png";
                    break;
                case ".dot":
                    iconPath = "/Images/FileTypeIcons/dot.png";
                    break;
                case ".dotx":
                    iconPath = "/Images/FileTypeIcons/dotx.png";
                    break;
                case ".dwg":
                    iconPath = "/Images/FileTypeIcons/dwg.png";
                    break;
                case ".dxf":
                    iconPath = "/Images/FileTypeIcons/dxf.png";
                    break;
                case ".eps":
                    iconPath = "/Images/FileTypeIcons/eps.png";
                    break;
                case ".exe":
                    iconPath = "/Images/FileTypeIcons/exe.png";
                    break;
                case ".flv":
                    iconPath = "/Images/FileTypeIcons/flv.png";
                    break;
                case ".gif":
                    iconPath = "/Images/FileTypeIcons/gif.png";
                    break;
                case ".h":
                    iconPath = "/Images/FileTypeIcons/h.png";
                    break;
                case ".html":
                    iconPath = "/Images/FileTypeIcons/html.png";
                    break;
                case ".ics":
                    iconPath = "/Images/FileTypeIcons/ics.png";
                    break;
                case ".iso":
                    iconPath = "/Images/FileTypeIcons/iso.png";
                    break;
                case ".java":
                    iconPath = "/Images/FileTypeIcons/java.png";
                    break;
                case ".jpg":
                    iconPath = "/Images/FileTypeIcons/jpg.png";
                    break;
                case ".key":
                    iconPath = "/Images/FileTypeIcons/key.png";
                    break;
                case ".m4v":
                    iconPath = "/Images/FileTypeIcons/m4v.png";
                    break;
                case ".mid":
                    iconPath = "/Images/FileTypeIcons/mid.png";
                    break;
                case ".mov":
                    iconPath = "/Images/FileTypeIcons/mov.png";
                    break;
                case ".mp3":
                    iconPath = "/Images/FileTypeIcons/mp3.png";
                    break;
                case ".mp4":
                    iconPath = "/Images/FileTypeIcons/mp4.png";
                    break;
                case ".mpg":
                    iconPath = "/Images/FileTypeIcons/mpg.png";
                    break;
                case ".odp":
                    iconPath = "/Images/FileTypeIcons/odp.png";
                    break;
                case ".ods":
                    iconPath = "/Images/FileTypeIcons/ods.png";
                    break;
                case ".odt":
                    iconPath = "/Images/FileTypeIcons/odt.png";
                    break;
                case ".otp":
                    iconPath = "/Images/FileTypeIcons/otp.png";
                    break;
                case ".ots":
                    iconPath = "/Images/FileTypeIcons/ots.png";
                    break;
                case ".ott":
                    iconPath = "/Images/FileTypeIcons/ott.png";
                    break;
                case ".pdf":
                    iconPath = "/Images/FileTypeIcons/pdf.png";
                    break;
                case ".php":
                    iconPath = "/Images/FileTypeIcons/php.png";
                    break;
                case ".png":
                    iconPath = "/Images/FileTypeIcons/png.png";
                    break;
                case ".pps":
                    iconPath = "/Images/FileTypeIcons/pps.png";
                    break;
                case ".ppt":
                    iconPath = "/Images/FileTypeIcons/ppt.png";
                    break;
                case ".psd":
                    iconPath = "/Images/FileTypeIcons/psd.png";
                    break;
                case ".py":
                    iconPath = "/Images/FileTypeIcons/py.png";
                    break;
                case ".qt":
                    iconPath = "/Images/FileTypeIcons/qt.png";
                    break;
                case ".rar":
                    iconPath = "/Images/FileTypeIcons/rar.png";
                    break;
                case ".rb":
                    iconPath = "/Images/FileTypeIcons/rb.png";
                    break;
                case ".rtf":
                    iconPath = "/Images/FileTypeIcons/rtf.png";
                    break;
                case ".sql":
                    iconPath = "/Images/FileTypeIcons/sql.png";
                    break;
                case ".tga":
                    iconPath = "/Images/FileTypeIcons/tga.png";
                    break;
                case ".tgz":
                    iconPath = "/Images/FileTypeIcons/tgz.png";
                    break;
                case ".tiff":
                    iconPath = "/Images/FileTypeIcons/tiff.png";
                    break;
                case ".txt":
                    iconPath = "/Images/FileTypeIcons/txt.png";
                    break;
                case ".wav":
                    iconPath = "/Images/FileTypeIcons/wav.png";
                    break;
                case ".xls":
                    iconPath = "/Images/FileTypeIcons/xls.png";
                    break;
                case ".xlsx":
                    iconPath = "/Images/FileTypeIcons/xlsx.png";
                    break;
                case ".xml":
                    iconPath = "/Images/FileTypeIcons/xml.png";
                    break;
                case ".yml":
                    iconPath = "/Images/FileTypeIcons/yml.png";
                    break;
                case ".zip":
                    iconPath = "/Images/FileTypeIcons/zip.png";
                    break;
                default:
                    iconPath = "/Images/FileTypeIcons//blank.png";
                    break;
            }

            return iconPath;
        }
    }
}



