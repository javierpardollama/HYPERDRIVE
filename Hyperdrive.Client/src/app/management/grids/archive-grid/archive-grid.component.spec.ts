import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchiveGridComponent } from './archive-grid.component';

describe('ArchiveGridComponent', () => {
  let component: ArchiveGridComponent;
  let fixture: ComponentFixture<ArchiveGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchiveGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchiveGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
