import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SignInAuthComponent } from './signin-auth.component';

describe('SignInAuthComponent', () => {
  let component: SignInAuthComponent;
  let fixture: ComponentFixture<SignInAuthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SignInAuthComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SignInAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
