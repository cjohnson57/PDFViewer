﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PDFApplication.Models;

namespace PDFApplication.Controllers
{
    public class HomeController : Controller
    {

        private CRASHDWHSRGEntities crashdb = new CRASHDWHSRGEntities();

        public ActionResult Index(string crashnum)
        {
            FactCrash dummy = new FactCrash();
            //0 indicates none entered, do not display anything
            dummy.CrashNumber = string.IsNullOrEmpty(crashnum) ? "0" : crashnum;
            return View(dummy);
        }

        public ActionResult GeneratePDF(FactCrash crash)
        {

            byte[] bytes = System.IO.File.ReadAllBytes("C:\\Users\\cjoh354\\Desktop\\test.pdf");
            return File(bytes, "application/pdf");
        }

        [ChildActionOnly]
        public ActionResult Partial(string crashnum)
        {
            FactCrash crash = new FactCrash();
            crash.CrashNumber = crashnum;
            //0 indicates none entered, do not display anything
            if (crashnum != "0")
            {
                try
                {
                    crash = crashdb.FactCrashes.Where(x => x.CrashNumber == crashnum).First();
                }
                catch
                {
                    //Could not find, -1 indicates error
                    crash.CrashNumber = "-1";
                }
            }
            return PartialView(crash);
        }

        [ChildActionOnly]
        [HttpPost]
        public ActionResult Partial([Bind(Include = "CrashNumber,SeverityCode,PredictedAlcohol,AccessControlCode,AgencySK,Aggressive,Alcohol,AlignmentCode,AmbulanceArrivalToHospital,AmbulanceArrivalToHospitalTime,AmbulanceArrive,AmbulanceArriveTime,AmbulanceCall,AmbulanceCallTime,AmbulanceHospital,AmbulanceHospitalTime,BAC08,Bicycle,BusLarge,BusSchool,BusSmall,ByPassCode,CitySK,CMV,CMVSeverityCode,ConstructionMaintenanceZone,ControlSection,CrashOrigin,CrashPK,CrashSeverityCode,CrashTime,DataChanged,DateEntered,DateSK,Distraction,DistrictZone,DOTDWorkZone,DOTDWorkZone5,Drugs,ElevatedInterstateCrash,Fatality,HazMatInvolved,HazMatReleasedInvolved,HighwayNumberSK,HighwayTypeCode,HitAndRun,Holiday,HSRGDate,HWYSectionsAK,Inattentive,Injury,IntersectingRoadName,Intersection,InvestigatingAgencyCode,LaneClosure,LaneClosureTime,LaneDeparture,Lat,LightingCode,LocationTypeCode,LogMile,Long,MannerCollisionCode,MilePost,Motorcycle,NoRestraint,OlderDriver,ParishCode,Pedestrian,PrimaryContributingFactorCode,PrimaryDirection,PrimaryDistance,PrimaryHighwayNumber,PrimaryMeasurementMiles,PrimaryRoadName,ProcessDate,PublicPropertyDamage,RailRoadTrainInvolved,RoadwayConditionCode,RoadwayDeparture,RoadwayRelationCode,RoadwayTypeCode,SecondaryContributingFactorCode,SecondaryDistance,SingleVehicle,SurfaceConditionCode,SurfaceTypeCode,Troop,TruckBusInvolvement,WeatherCode,YoungDriver")] FactCrash crash)
        {
            if (ModelState.IsValid)
            {
                crashdb.Entry(crash).State = System.Data.Entity.EntityState.Modified;
                crashdb.SaveChanges();
            }
            return PartialView(crash);
        }
    }
}