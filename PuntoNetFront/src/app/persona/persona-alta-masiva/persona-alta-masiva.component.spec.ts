import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonaAltaMasivaComponent } from './persona-alta-masiva.component';

describe('PersonaAltaMasivaComponent', () => {
  let component: PersonaAltaMasivaComponent;
  let fixture: ComponentFixture<PersonaAltaMasivaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonaAltaMasivaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonaAltaMasivaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
