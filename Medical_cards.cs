//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Medical_cards
    {
        public int ID { get; set; }
        public int PersonnelNumber { get; set; }
        public string Name_group { get; set; }
        public int Insurance_OMC { get; set; }
        public string Snils { get; set; }
    
        public virtual Generalinformation Generalinformation { get; set; }
    }
}