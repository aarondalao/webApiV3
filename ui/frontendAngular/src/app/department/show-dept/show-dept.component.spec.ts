import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowDeptComponent } from './show-dept.component';

describe('ShowDeptComponent', () => {
  let component: ShowDeptComponent;
  let fixture: ComponentFixture<ShowDeptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowDeptComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowDeptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
