import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeEmailSecurityComponent } from './changeemail-security.component';

describe('ChangeemailSecurityComponent', () => {
  let component: ChangeEmailSecurityComponent;
  let fixture: ComponentFixture<ChangeEmailSecurityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeEmailSecurityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeEmailSecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
