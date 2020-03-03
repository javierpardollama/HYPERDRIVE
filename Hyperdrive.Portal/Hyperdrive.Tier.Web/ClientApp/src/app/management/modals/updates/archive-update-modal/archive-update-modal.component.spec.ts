import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchiveUpdateModalComponent } from './archive-update-modal.component';

describe('ArchiveUpdateModalComponent', () => {
  let component: ArchiveUpdateModalComponent;
  let fixture: ComponentFixture<ArchiveUpdateModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchiveUpdateModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchiveUpdateModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
