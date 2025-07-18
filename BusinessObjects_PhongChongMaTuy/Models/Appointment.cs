﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BusinessObjects_PhongChongMaTuy.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ConsultantId { get; set; }

    public DateOnly? AppointmentDate { get; set; }

    public int? SlotId { get; set; }

    public string Status { get; set; }

    public string Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Consultant { get; set; }

    public virtual Slot Slot { get; set; }

    public virtual User User { get; set; }
}