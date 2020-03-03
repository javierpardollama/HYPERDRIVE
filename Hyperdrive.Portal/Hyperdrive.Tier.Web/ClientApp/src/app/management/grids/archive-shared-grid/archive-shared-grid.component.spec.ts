import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchiveSharedGridComponent } from './archive-shared-grid.component';

describe('ArchiveSharedGridComponent', () => {
  let component: ArchiveSharedGridComponent;
  let fixture: ComponentFixture<ArchiveSharedGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchiveSharedGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchiveSharedGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
