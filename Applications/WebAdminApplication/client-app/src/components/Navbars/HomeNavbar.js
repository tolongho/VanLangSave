// nodejs library that concatenates classes
import classnames from 'classnames';
// nodejs library to set properties for components
import PropTypes from 'prop-types';
import { useSelector } from 'react-redux';
// reactstrap components

import { Link } from 'react-router-dom';
import {
  Collapse,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
  DropdownToggle,
  FormGroup,
  Form,
  Input,
  InputGroupAddon,
  InputGroupText,
  InputGroup,
  ListGroupItem,
  ListGroup,
  Navbar,
  NavItem,
  NavLink,
  Nav,
  Container,
  Row,
  Col,
} from 'reactstrap';
import DropdownProfile from 'views/pages/home/components/DropdownProfile';
import MenuNav from 'views/pages/home/components/MenuNav';

function HomeNavbar({ theme, sidenavOpen, toggleSidenav }) {
  // function that on mobile devices makes the search open
  const openSearch = () => {
    document.body.classList.add('g-navbar-search-showing');
    setTimeout(function () {
      document.body.classList.remove('g-navbar-search-showing');
      document.body.classList.add('g-navbar-search-show');
    }, 150);
    setTimeout(function () {
      document.body.classList.add('g-navbar-search-shown');
    }, 300);
  };
  // function that on mobile devices makes the search close
  const closeSearch = () => {
    document.body.classList.remove('g-navbar-search-shown');
    setTimeout(function () {
      document.body.classList.remove('g-navbar-search-show');
      document.body.classList.add('g-navbar-search-hiding');
    }, 150);
    setTimeout(function () {
      document.body.classList.remove('g-navbar-search-hiding');
      document.body.classList.add('g-navbar-search-hidden');
    }, 300);
    setTimeout(function () {
      document.body.classList.remove('g-navbar-search-hidden');
    }, 500);
  };
  const userProfile = useSelector((state) => state.login.userProfile);
  return (
    <>
      <Navbar
        className={classnames(
          'navbar-top navbar-expand border-bottom bg-white',
        )}
      >
        <Link className="navbar-brand-custom" to="/">
          <img alt="..." src={require('assets/img/brand/logo.png').default} />
        </Link>
        <Container fluid>
          <Collapse navbar isOpen={true}>
            <Form
              className={classnames(
                'navbar-search form-inline mr-sm-3',
                { 'navbar-search-light': theme === 'dark' },
                { 'navbar-search-dark': theme === 'light' },
              )}
            >
              <FormGroup className="mb-0">
                <InputGroup className="input-group-alternative input-group-merge">
                  <InputGroupAddon addonType="prepend">
                    <InputGroupText>
                      <i className="fas fa-search" />
                    </InputGroupText>
                  </InputGroupAddon>
                  <Input placeholder="Search" type="text" />
                </InputGroup>
              </FormGroup>
              <button
                aria-label="Close"
                className="close"
                type="button"
                onClick={closeSearch}
              >
                <span aria-hidden={true}>×</span>
              </button>
            </Form>

            <Nav className="align-items-center ml-md-auto" navbar>
              <NavItem className="d-sm-none">
                <NavLink onClick={openSearch}>
                  <i className="ni ni-zoom-split-in" />
                </NavLink>
              </NavItem>
              <UncontrolledDropdown nav>
                <DropdownToggle className="nav-link" color="" tag="a">
                  <i className="ni ni-bell-55" />
                </DropdownToggle>
                <DropdownMenu
                  className="dropdown-menu-xl py-0 overflow-hidden"
                  right
                >
                  <div className="px-3 py-3">
                    <h6 className="text-sm text-muted m-0">
                      You have <strong className="text-info">13</strong>{' '}
                      notifications.
                    </h6>
                  </div>

                  <ListGroup flush>
                    <ListGroupItem
                      className="list-group-item-action"
                      href="#pablo"
                      onClick={(e) => e.preventDefault()}
                      tag="a"
                    >
                      <Row className="align-items-center">
                        <Col className="col-auto">
                          <img
                            alt="..."
                            className="avatar rounded-circle"
                            src={
                              userProfile.avatarURL
                                ? userProfile.avatarURL
                                : 'https://www.dropbox.com/s/t4jamyq65xt41uo/t3ohtfyk.cjw.png?dl=1'
                            }
                          />
                        </Col>
                        <div className="col ml--2">
                          <div className="d-flex justify-content-between align-items-center">
                            <div>
                              <h4 className="mb-0 text-sm">
                                {userProfile.lastName +
                                  ' ' +
                                  userProfile.firstName}
                              </h4>
                            </div>
                            <div className="text-right text-muted">
                              <small>2 hrs ago</small>
                            </div>
                          </div>
                          <p className="text-sm mb-0">
                            Let's meet at Starbucks at 11:30. Wdyt?
                          </p>
                        </div>
                      </Row>
                    </ListGroupItem>
                    <ListGroupItem
                      className="list-group-item-action"
                      href="#pablo"
                      onClick={(e) => e.preventDefault()}
                      tag="a"
                    >
                      <Row className="align-items-center">
                        <Col className="col-auto">
                          <img
                            alt="..."
                            className="avatar rounded-circle"
                            src={require('assets/img/theme/team-2.jpg').default}
                          />
                        </Col>
                        <div className="col ml--2">
                          <div className="d-flex justify-content-between align-items-center">
                            <div>
                              <h4 className="mb-0 text-sm">John Snow</h4>
                            </div>
                            <div className="text-right text-muted">
                              <small>3 hrs ago</small>
                            </div>
                          </div>
                          <p className="text-sm mb-0">
                            A new issue has been reported for Argon.
                          </p>
                        </div>
                      </Row>
                    </ListGroupItem>
                    <ListGroupItem
                      className="list-group-item-action"
                      href="#pablo"
                      onClick={(e) => e.preventDefault()}
                      tag="a"
                    >
                      <Row className="align-items-center">
                        <Col className="col-auto">
                          <img
                            alt="..."
                            className="avatar rounded-circle"
                            src={require('assets/img/theme/team-3.jpg').default}
                          />
                        </Col>
                        <div className="col ml--2">
                          <div className="d-flex justify-content-between align-items-center">
                            <div>
                              <h4 className="mb-0 text-sm">John Snow</h4>
                            </div>
                            <div className="text-right text-muted">
                              <small>5 hrs ago</small>
                            </div>
                          </div>
                          <p className="text-sm mb-0">
                            Your posts have been liked a lot.
                          </p>
                        </div>
                      </Row>
                    </ListGroupItem>
                    <ListGroupItem
                      className="list-group-item-action"
                      href="#pablo"
                      onClick={(e) => e.preventDefault()}
                      tag="a"
                    >
                      <Row className="align-items-center">
                        <Col className="col-auto">
                          <img
                            alt="..."
                            className="avatar rounded-circle"
                            src={require('assets/img/theme/team-4.jpg').default}
                          />
                        </Col>
                        <div className="col ml--2">
                          <div className="d-flex justify-content-between align-items-center">
                            <div>
                              <h4 className="mb-0 text-sm">John Snow</h4>
                            </div>
                            <div className="text-right text-muted">
                              <small>2 hrs ago</small>
                            </div>
                          </div>
                          <p className="text-sm mb-0">
                            Let's meet at Starbucks at 11:30. Wdyt?
                          </p>
                        </div>
                      </Row>
                    </ListGroupItem>
                    <ListGroupItem
                      className="list-group-item-action"
                      href="#pablo"
                      onClick={(e) => e.preventDefault()}
                      tag="a"
                    >
                      <Row className="align-items-center">
                        <Col className="col-auto">
                          <img
                            alt="..."
                            className="avatar rounded-circle"
                            src={require('assets/img/theme/team-5.jpg').default}
                          />
                        </Col>
                        <div className="col ml--2">
                          <div className="d-flex justify-content-between align-items-center">
                            <div>
                              <h4 className="mb-0 text-sm">John Snow</h4>
                            </div>
                            <div className="text-right text-muted">
                              <small>3 hrs ago</small>
                            </div>
                          </div>
                          <p className="text-sm mb-0">
                            A new issue has been reported for Argon.
                          </p>
                        </div>
                      </Row>
                    </ListGroupItem>
                  </ListGroup>

                  <DropdownItem
                    className="text-center text-info font-weight-bold py-3"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                  >
                    View all
                  </DropdownItem>
                </DropdownMenu>
              </UncontrolledDropdown>
              <MenuNav></MenuNav>
            </Nav>
            <Nav className="align-items-center ml-auto ml-md-0" navbar>
              <DropdownProfile
                userName={userProfile.lastName + ' ' + userProfile.firstName}
                avartarURL={
                  userProfile.avatarURL
                    ? userProfile.avatarURL
                    : 'https://www.dropbox.com/s/t4jamyq65xt41uo/t3ohtfyk.cjw.png?dl=1'
                }
              ></DropdownProfile>
            </Nav>
          </Collapse>
        </Container>
      </Navbar>
    </>
  );
}

HomeNavbar.defaultProps = {
  toggleSidenav: () => {},
  sidenavOpen: false,
  theme: 'dark',
};
HomeNavbar.propTypes = {
  toggleSidenav: PropTypes.func,
  sidenavOpen: PropTypes.bool,
  theme: PropTypes.oneOf(['dark', 'light']),
};

export default HomeNavbar;