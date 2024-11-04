import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DayIntervalsModel } from '../models/day-intervals-model';
import { IntervalModel } from '../models/interval-model';
import { GroupModel } from '../models/group-model';
import { FileService } from '../services/file-service';

@Component({
  selector: 'schedule-table-component',
  templateUrl: './schedule-table-component.component.html',
  styleUrl: './schedule-table-component.component.css',
})
export class ScheduleTableComponentComponent implements OnInit {
  public intervalList: IntervalModel[];
  public groupsList: GroupModel[];

  public dayIntervalList: DayIntervalsModel[];

  public selectedGroupValue: string;
  public fileName: string;
  public scheduleFromFile: DayIntervalsModel[];

  public isDataChanged: boolean = false;

  constructor(private _httpClient: HttpClient, private _fileService: FileService) { }

  ngOnInit() {
    this.initIntervals();
    this.initGroups();
  }

  initIntervals() {
    this._httpClient.get<DayIntervalsModel[]>('api/Interval').subscribe(result => {

      this.dayIntervalList = result;
      this.intervalList = this.dayIntervalList[0].intervals;
    }
    );
  }

  initGroups() {
    this._httpClient.get<GroupModel[]>('api/Group').subscribe(result => {
      this.groupsList = result;
    }
    );
  }

  selectInterval(day: string, interval: IntervalModel, isActive: boolean) {
    if (!this.selectedGroupValue) {
      return;
    }

    this.isDataChanged = true;

    var daySchedule = this.dayIntervalList.find(x => x.day == day);
    if (daySchedule) {
      var selectedInterval = daySchedule.intervals.find(x => x.startTime == interval.startTime)

      if (selectedInterval) {
        selectedInterval.isActive = !selectedInterval.isActive;
      }
    }
  }

  filterStreet(value: string) {
    var request = {
      address: value
    }

    this.initIntervals();

    this._httpClient.post<DayIntervalsModel[]>('api/Schedule/FindByAddres', request).subscribe(result => {
      console.log(result)

      if (!result || result.length == 0) {
        alert("Street Not Found");
      }

      this.updateCurrentData(result);
    })
  }

  groupSelected() {
    var request = {
      groupId: this.selectedGroupValue
    }

    this.initIntervals();

    this._httpClient.post<DayIntervalsModel[]>('api/Schedule/FindByGroup', request).subscribe(result => {
      console.log(result)
      this.updateCurrentData(result);
    })
  }

  updateCurrentData(input: DayIntervalsModel[]) {

    var currentDate = new Date();
    var dayNow = currentDate.getDay();
    var currentHour = currentDate.toTimeString().slice(0, 2);
    var isDarkNow: boolean = false;

    input.forEach(resultDay => {
      var activeDay = this.dayIntervalList.filter(x => x.day == resultDay.day).shift();

      if (!this.selectedGroupValue && activeDay?.groupId) {
        this.selectedGroupValue = activeDay?.groupId;
      }

      resultDay.intervals.forEach(resultInterval => {
        var intervalsToActivate = activeDay?.intervals.filter(x => x.startTime == resultInterval.startTime);
        intervalsToActivate?.forEach(interval => {
          interval.isActive = resultInterval.isActive;
          if (dayNow == resultDay.dayNumber && currentHour == interval.startTime.slice(0, 2) && resultInterval.isActive) {
            isDarkNow = true;
          }
        });
      });
    });

    if (isDarkNow) {
      alert("No Power Now");
    }

    if (!isDarkNow) {
      alert("No issues detected");
    }
  }

  saveChanges() {
    var request = {
      scheduleModelsList: this.dayIntervalList,
      groupId: this.selectedGroupValue
    }

    this._httpClient.post('api/Schedule/SaveSchedule', request).subscribe(result =>
      console.log(result)
    );
  }

  saveToJson() {
    this._fileService.saveArrayAsJsonFile(this.dayIntervalList, "Schedule_test")
  }

  onFileSelected(event: any) {
    var file: File = event.target.files[0];
    this.fileName = file.name;
    var alertMessage = "Wrong format";

    this._fileService.readXLSXFile(file).subscribe(result => {

      result.forEach(fileString => {
        var stringPartsByDot = fileString.split('.');
        if (stringPartsByDot.length > 2) {
          alert(alertMessage)
          return;
        }
        var intervals = stringPartsByDot[1].split(';');
        if (intervals.length < 1) {
          alert(alertMessage)
          return;
        }

        var dayNumber: number = 0;
        intervals.forEach(stringInterval => {
          var splitedInterval = stringInterval.split('-');

          if (splitedInterval.length > 2) {
            alert(alertMessage)
            return;
          }

          var newDayIntervals = new DayIntervalsModel();
          newDayIntervals.groupNumber = parseInt(stringPartsByDot[0]);
          newDayIntervals.dayNumber = dayNumber;

          var startTimeString = splitedInterval[0];
          var endTimeString = splitedInterval[1];

          var splitedStartTimeString = startTimeString.split(':');
          var splitedEndTimeString = endTimeString.split(':');

          var startHour = parseInt(splitedStartTimeString[0]);
          var endHour = parseInt(splitedEndTimeString[0]);

          for (let i = 0; i < endHour - startHour; i++) {
            var newInterval = new IntervalModel();

            newInterval.startTime = (startHour + i).toString() + ":00";
            newInterval.endTime = (startHour + i + 1).toString() + ":00";

            if (!newDayIntervals.intervals) {
              newDayIntervals.intervals = new Array<IntervalModel>();
            }

            newDayIntervals.intervals.push(newInterval);
          }

          if (!this.scheduleFromFile)
          {
            this.scheduleFromFile = new Array<DayIntervalsModel>();
          }

          this.scheduleFromFile.push(newDayIntervals);
          dayNumber++;
        })
      });
    });
  }

  saveScheduleFromFile() {
    var request = {
      items: this.scheduleFromFile
    }
    
    this._httpClient.post<any>('api/Schedule/SaveScheduleFromFile', request).subscribe(result => {

      if (result.error) {
        alert(result.error);
      }
    });
  }
}

