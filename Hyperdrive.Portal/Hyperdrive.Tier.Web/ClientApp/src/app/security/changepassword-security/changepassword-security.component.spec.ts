import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangePasswordSecurityComponent } from './changepassword-security.component';

describe('ChangepasswordSecurityComponent', () => {
  let component: ChangePasswordSecurityComponent;
  let fixture: ComponentFixture<ChangePasswordSecurityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangePasswordSecurityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangePasswordSecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
