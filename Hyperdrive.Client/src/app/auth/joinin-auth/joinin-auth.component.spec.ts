import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinInAuthComponent } from './joinin-auth.component';

describe('JoinInAuthComponent', () => {
  let component: JoinInAuthComponent;
  let fixture: ComponentFixture<JoinInAuthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JoinInAuthComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JoinInAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
