import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchiveAddModalComponent } from './archive-add-modal.component';

describe('ArchiveAddModalComponent', () => {
  let component: ArchiveAddModalComponent;
  let fixture: ComponentFixture<ArchiveAddModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchiveAddModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchiveAddModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
