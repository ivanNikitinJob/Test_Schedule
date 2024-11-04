import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleTableComponentComponent } from './schedule-table-component.component';

describe('ScheduleTableComponentComponent', () => {
  let component: ScheduleTableComponentComponent;
  let fixture: ComponentFixture<ScheduleTableComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ScheduleTableComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ScheduleTableComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
