import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboradGetNoticiaComponent } from './dashborad-get-noticia.component';

describe('DashboradGetNoticiaComponent', () => {
  let component: DashboradGetNoticiaComponent;
  let fixture: ComponentFixture<DashboradGetNoticiaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboradGetNoticiaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboradGetNoticiaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
