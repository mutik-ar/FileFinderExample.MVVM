using System.IO;

namespace Interfaces.Model
{
    /// <summary>
    /// Класс описывает элемент с информацией о диске для выпадающего списка на главной форме
    /// </summary>
    public class DriveInfoItem
    {
        /// <summary>
        /// хранит имя диска (например: "C:\\")
        /// </summary>
        public string DriveName { get; set; }

        /// <summary>
        /// хранит имя метки для диска, установлена в системе (например: "Мой системный диск")
        /// </summary>
        public string DriveVolumeLabel { get; set; }

        /// <summary>
        /// хранит текстовое представление формата диска (NTFS или FAT32)
        /// </summary>
        public string DriveFormat { get; set; }

        /// <summary>
        /// хранит текстовое представление типа диска.
        /// См. метод GetDriveTypeAsString() для возможных значений свойства
        /// </summary>
        public string DriveTypeString { get; set; }

        /// <summary>
        /// Хранит общее свободное пространство на диске, в Гигабайтах
        /// </summary>
        public long TotalFreeSpaceGb { get; set; }

        /// <summary>
        /// Хранит общий размер диска, в Гигабайтах
        /// </summary>
        public long TotalSizeGb { get; set; }

        /// <summary>
        /// Хранит доступное свободное пространство, в Гигабайтах
        /// </summary>
        public long AvailableFreeSpaceGb { get; set; }

        /// <summary>
        /// Конструктор класса, создаёт экземпляр класса по входному параметру <paramref name="driveInfo"/>
        /// </summary>
        /// <param name="driveInfo">входной экземпляр типа DriveInfo, на основе которого необходимо создать экземпляр класса DriveInfoItem</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DriveInfoItem(DriveInfo driveInfo)
        {
            if (driveInfo == null)
            {
                throw new ArgumentNullException("driveInfo", "Ошибка: параметр не может быть null!");
            }

            DriveName = driveInfo.Name;
            DriveVolumeLabel = driveInfo.VolumeLabel;
            DriveFormat = driveInfo.DriveFormat;
            DriveTypeString = GetDriveTypeAsString(driveInfo.DriveType);

            TotalFreeSpaceGb = GetSizeInGigabytes(driveInfo.TotalFreeSpace);
            TotalSizeGb = GetSizeInGigabytes(driveInfo.TotalSize);
            AvailableFreeSpaceGb = GetSizeInGigabytes(driveInfo.AvailableFreeSpace);
        }

        /// <summary>
        /// Переводит размер из байт в Гигабайты
        /// </summary>
        /// <param name="size">размер, в байтах</param>
        /// <returns>целое число, размер в Гигабайтах</returns>
        private long GetSizeInGigabytes(long size)
        {
            return size / 1_073_741_824;
        }

        /// <summary>
        /// Возвращает часть описания диска, связанного с общим/доступным/свободным объемом дискового пространства
        /// </summary>
        /// <returns>строка, содержащая детали по занимаемому и свободному месту на диске</returns>
        private string GetVolumeSizeString()
        {
            return string.Format("Объём: {0}Гб, Всего свободно: {1}Гб, Доступно: {2}Гб", TotalSizeGb, TotalFreeSpaceGb, AvailableFreeSpaceGb);
        }

        /// <summary>
        /// Переопределение метода в целях отображения текста в заданном формате в выпадающем списке с дисками на главной форме
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetReadableDriveName() + ": " + DriveTypeString + ", " + DriveFormat + ", " + GetVolumeSizeString();
        }

        /// <summary>
        /// Возвращает читаемое имя диска для его представления в выпадающем списке.
        /// Если метка диска не задана, вернёт строку в формате "[имя_диска]:\\", например: "[C:\\]"
        /// Если метка диска задана, вернёт строку в формате "[имя_метки_диска] имя_диска:\\", например: "[Мой системный диск] C:\\"
        /// </summary>
        /// <returns>строка, содержащая имя диска или метку и имя диска, если метка установлена</returns>
        private string GetReadableDriveName()
        {
            if (DriveVolumeLabel == null || DriveVolumeLabel.Length == 0)
            {
                return "[" + DriveName + "]";
            }
            return "[" + DriveVolumeLabel + "] " + DriveName;
        }

        /// <summary>
        /// Возвращает текстовое представление для различных типов дисков, которые могут быть в системе
        /// </summary>
        /// <param name="driveType"></param>
        /// <returns></returns>
        private string GetDriveTypeAsString(DriveType driveType)
        {
            switch (driveType)
            {
                case DriveType.Fixed:
                    return "Фиксированный диск";
                case DriveType.Network:
                    return "Сетевой диск";
                case DriveType.Removable:
                    return "Съёмный диск";
                case DriveType.Ram:
                    return "ОЗУ";
                case DriveType.NoRootDirectory:
                    return "Без корневого каталога";
                case DriveType.CDRom:
                    return "CD-ROM";
                case DriveType.Unknown:
                default:
                    return "Неизвестно";
            }
        }
    }
}
