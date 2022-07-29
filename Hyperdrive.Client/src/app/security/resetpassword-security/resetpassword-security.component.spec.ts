import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetPasswordSecurityComponent } from './resetpassword-security.component';

describe('ResetpasswordSecurityComponent', () => {
  let component: ResetPasswordSecurityComponent;
  let fixture: ComponentFixture<ResetPasswordSecurityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResetPasswordSecurityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetPasswordSecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
