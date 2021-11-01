import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEdificioComponent } from './user-edificio.component';

describe('UserEdificioComponent', () => {
  let component: UserEdificioComponent;
  let fixture: ComponentFixture<UserEdificioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserEdificioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserEdificioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
